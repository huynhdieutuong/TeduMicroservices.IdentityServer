﻿namespace TeduMicroservices.IDP.Infrastructure.Common;

public static class PermissionHelper
{
    public static string GetPermission(string functionCode, string commandCode) => string.Join(".", functionCode, commandCode);
}
