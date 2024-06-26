IF EXISTS (
	SELECT * FROM sysobjects
	WHERE id = object_id(N'[Get_Permission_By_RoleId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE [Get_Permission_By_RoleId]
END
GO

CREATE PROCEDURE [Get_Permission_By_RoleId] @roleId varchar(50) null
AS
BEGIN
	SET NOCOUNT ON; -- to prevent extra result sets from interfering with SELECT statements.
	SELECT * FROM [Identity].Permissions
	WHERE RoleId = @roleId
END