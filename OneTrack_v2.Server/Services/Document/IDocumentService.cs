﻿using DataModel.Response;

namespace OneTrak_v2.Services
{
    public interface IDocumentService
    {
        Task<ReturnResult> Upload(string vFilePathUri, Stream vStream);
    }
}