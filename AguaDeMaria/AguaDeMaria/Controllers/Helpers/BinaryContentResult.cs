﻿using System.Web;
using System.Web.Mvc;
using System.IO;

namespace AguaDeMaria.Controllers.Helpers
{
    public class BinaryContentResult : ActionResult
    {
        private string ContentType;
        private byte[] ContentBytes;

        public BinaryContentResult(byte[] contentBytes, string contentType)
        {
            ContentBytes = contentBytes;
            ContentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = ContentType;

            var stream = new MemoryStream(ContentBytes);
            stream.WriteTo(response.OutputStream);
            stream.Dispose();
        }
    }
}