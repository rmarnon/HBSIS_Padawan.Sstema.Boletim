using System.Collections.Generic;
using System.Net;

namespace HBSIS_Padawan.Sistema.Boletim.Util
{
    public class Result<T>
    {
        public T Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public bool Error { get; set; }
        public List<string> Message { get; set; } = new List<string>();
    }
}
