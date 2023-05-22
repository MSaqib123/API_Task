using System;
using System.Collections.Generic;
using System.Text;

namespace DML
{
    public class clsCategory: clsCommon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
