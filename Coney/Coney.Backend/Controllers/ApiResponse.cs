﻿namespace Coney.Backend.Controllers;

public class ApiResponse<T>
{
    public bool Status { get; set; }
    public int Code { get; set; }
    public T Data { get; set; }

    public ApiResponse(bool status, int code, T data)
    {
        Status = status;
        Code = code;
        Data = data;
    }
}