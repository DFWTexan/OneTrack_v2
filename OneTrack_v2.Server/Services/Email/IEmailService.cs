﻿using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrak_v2.DataModel;
using OneTrak_v2.Services.Model;

namespace OneTrack_v2.Services
{
    public interface IEmailService
    {
        public ReturnResult GetEmailComTemplates();
        public ReturnResult GetEmailTemplate(int vCommunicationID, int vEmploymentID);
        public ReturnResult Send([FromBody] IputSendEmail vInput);
    }
}
