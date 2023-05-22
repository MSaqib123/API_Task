using System;
using System.Collections.Generic;
using System.Text;

namespace DML
{
    public class clsResult: clsCommon
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }

}
