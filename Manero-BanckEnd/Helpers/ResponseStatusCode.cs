using Microsoft.AspNetCore.Http.HttpResults;

namespace Manero_BanckEnd.Helpers
{
    public enum ResponseStatusCode
    {
        OK = 200,
        CREATED = 201,
        NOTFOUND = 404,
        EXIST = 409,
        ERROR = 500,
        BADREQUEST = 400,
        UNAUTHORIZED = 401,

    }
}
