﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DML
{
    public class clsProduct: clsCommon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool Status { get; set; } = false;
    }
}
