USE IManage
GO

/****** Inserting Contents into RoleType Table ******/
INSERT INTO role_type("name")
VALUES ('System role');
INSERT INTO role_type("name")
VALUES ('Project role');

/****** Inserting Contents into FunctionRightType Table ******/
INSERT INTO function_right_type ("name")
VALUES ('System permissions');
INSERT INTO function_right_type ("name")
VALUES ('Engineering permissions');

/****** Inserting Contents into User Table ******/
INSERT INTO "user" (email_id, "name", full_name, avatar_url, is_exfactory)
VALUES ('admin@imanage.com', 'admin', 'admin','https://s.gravatar.com/avatar/c11dfef4c471ed4c9c394f56e8ebb5d4?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fad.png',1);

/****** Inserting Contents into Role Table ******/
INSERT INTO "role" ("name", description, type_id, is_exfactory)
VALUES ('IManage Admin', 'IManage Admin', 1, 1);
INSERT INTO "role" ("name", description, type_id, is_exfactory)
VALUES ('IManage Engineer', 'IManage Engineer', 2, 1);

/****** Inserting Contents into UserRoleMapping Table ******/
INSERT INTO user_role_mapping (user_id, role_id)
VALUES (1,1);

/****** Inserting Contents into FunctionRight Table ******/
INSERT INTO  function_right ("name", description)
VALUES ('im.user.add', 'Create IManage user');
INSERT INTO  function_right ( "name", description)
VALUES ('im.user.delete', 'Delete IManage user');
INSERT INTO  function_right ( "name", description)
VALUES ('im.user.view', 'View IManage users');
INSERT INTO  function_right ( "name", description)
VALUES ('im.user.edit', 'Modify IManage user');
INSERT INTO  function_right ( "name", description)
VALUES ('im.user.manage', 'Manage IManage user');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.users.add', 'Add IManage user to project');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.users.delete', 'Delete IManage user from project');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.users.view', 'View project users');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.users.manage', 'Manage users of a project');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.users.edit', 'Modify project users');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.add', 'Create project');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.delete', 'Delete project');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.view', 'View project');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.edit', 'Modify project');
INSERT INTO  function_right ( "name", description)
VALUES ('im.project.manage', 'Manage project');
INSERT INTO  function_right ( "name", description)
VALUES ('im.role.view', 'View Roles');
INSERT INTO  function_right ( "name", description)
VALUES ('im.functionrights.view', 'View function rights');
INSERT INTO  function_right ("name", description)
VALUES ('im.role.delete', 'Delete Roles');

/****** Inserting Contents into FunctionRight & Type Mapping Table ******/
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (1, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (2, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (3, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (4, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (5, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (6, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (7, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (8, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (9, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (10, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (11, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (11, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (12, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (12, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (13, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (13, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (14, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (14, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (15, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (15, 2);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (16, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (17, 1);
INSERT INTO function_right_type_mapping (functionrightID, typeID)
VALUES (18, 2);

/****** Inserting Contents into RoleFunctionRightMapping Table ******/
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 1);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 2);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 3);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 4);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 5);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 6);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 7);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 8);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 9);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 10);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 11);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 12);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 13);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 14);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 15);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 16);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 17);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 18);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (1, 19);
INSERT INTO role_function_right_mapping (role_id, function_right_id)
VALUES (2, 18);