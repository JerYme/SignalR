﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Microsoft.AspNet.SignalR.Client.Http
{
    public class HttpRequestMessageWrapper : IRequest
    {
        private readonly HttpRequestMessage _httpRequestMessage;
        private readonly Action _cancel;

        public HttpRequestMessageWrapper(HttpRequestMessage httpRequestMessage, Action cancel)
        {
            _httpRequestMessage = httpRequestMessage;
            _cancel = cancel;
        }

        public string UserAgent { get; set; }

        public string Accept { get; set; }

        public void Abort()
        {
            _cancel();
        }

        public void SetRequestHeaders(IDictionary<string, string> headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }

            if (UserAgent != null)
            {
                // TODO: Fix format of user agent so that ProductInfoHeaderValue likes it
                // request.Headers.UserAgent.Add(new ProductInfoHeaderValue(UserAgent));
            }

            if (Accept != null)
            {
                _httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Accept));
            }

            foreach (KeyValuePair<string, string> headerEntry in headers)
            {
                _httpRequestMessage.Headers.Add(headerEntry.Key, headerEntry.Value);
            }
        }
    }
}
