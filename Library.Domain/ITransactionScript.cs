﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain
{
    public interface ITransactionScript<T>
    {
        T Output { get; }
        void Execute();
    }
}
