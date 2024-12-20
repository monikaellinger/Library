﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.DataTransferObject;
using Library.Data.TableDataGateWay;

namespace Library.Domain.BookTransactionSctipts
{
    public class GetBookTransactionScript : ITransactionScript<BookDTO>
    {
        public BookDTO Output { get; private set; }
        public int BookID { get; set; }

        public void Execute()
        {
            var bookTDG = new BookTDGW();
            Output = bookTDG.GetBook(BookID);
        }
    }
}
