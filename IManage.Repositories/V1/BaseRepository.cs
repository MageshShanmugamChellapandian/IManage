using IManage.ErrorHandling.ApiExceptions;
using IManage.Repositories.V1.GraphVertices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;

namespace IManage.Repositories.V1
{
    /// <summary>
    /// Base repository instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IDisposable
    {
        #region Private fields.

        private bool _disposed;
        private readonly IDriver _driver;
        private readonly ILogger<BaseRepository<T>> _logger;
        private readonly IStringLocalizer<BaseRepository<T>> _localizer;
        private readonly IConfiguration _config;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public BaseRepository(IDriver driver,ILogger<BaseRepository<T>> logger, IStringLocalizer<BaseRepository<T>> localizer, IConfiguration config) 
        {
            _driver = driver;
            _logger = logger;
            _localizer = localizer;
            _config = config;
        }

        #endregion

        #region Public methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Executes the query and return a single record.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual async Task<List<T>> ExecuteReadTransactionAndReturnRecordsAsync(string queryString, IDictionary<string, object> parameters)
        {
            try
            {
                using (var session = _driver.AsyncSession(o => o.WithDatabase(_config.GetSection("Neo4jConfig:Database").Value)))
                {
                    var result = await session.RunAsync(queryString, parameters);
                    var record = await result.ToListAsync(ConvertToTypeSafe);

                    return record;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_localizer["DatabaseException"].Value, ex);
                throw new InternalServerException(_localizer["DatabaseException"].Value, ex);
            }
        }

        /// <summary>
        /// Executes write action and returns the record.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="InternalServerException"></exception>
        protected virtual async Task<T> ExecuteWriteTransactionAndReturnRecordAsync(string queryString, IDictionary<string, object> parameters)
        {
            try
            {
                using (var session = _driver.AsyncSession(o => o.WithDatabase(_config.GetSection("Neo4jConfig:Database").Value)))
                {
                    var result = await session.RunAsync(queryString, parameters);
                    var record = await result.SingleAsync(ConvertToTypeSafe);

                    return record;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(_localizer["DatabaseException"].Value, ex);
                throw new InternalServerException(_localizer["DatabaseException"].Value, ex);
            }
        }

        /// <summary>
        /// Obejct disposal.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _driver?.Dispose();
            }

            _disposed = true;
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~BaseRepository() 
        {
            Dispose(false);
        }

        #endregion

        #region abstract methods

        /// <summary>
        /// Abstract method for conversion.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        protected abstract T ConvertToTypeSafe(IRecord record);

        #endregion
    }
}
