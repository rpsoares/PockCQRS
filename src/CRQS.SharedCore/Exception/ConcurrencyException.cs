﻿using System;

namespace CRQS.SharedCore.Exception
{
    public class ConcurrencyException : System.Exception
    {
        public ConcurrencyException(Guid id)
            : base(string.Format("A different version than expected was found in aggregate {0}", id))
        {
        }
    }
}