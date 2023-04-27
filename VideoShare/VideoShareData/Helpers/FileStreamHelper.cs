﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using VideoShareData.Models;
using VideoShareData.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace VideoShareData.Helpers
{
    //This class queries for the current transaction context and filepath of a FileStream data object.
    //It allows the user to asynchronously get and set the FileStream files for a data object by calling
    //"async SetFileStreamData()" and "async GetFileStreamData()" methods
    //Currently only works for objects that have a single FileStream column
    internal class FileStreamHelper
    {
        DbContext context { get; set; }
        FileStreamDataObj fileDataObj { get; set; }
        public FileStreamRowInfo rowInfo { get; }

        public FileStreamHelper(DbContext initialContext, FileStreamDataObj dataObj) {
            context = initialContext;
            fileDataObj = dataObj;
            Object[] parameters = {
                new Microsoft.Data.SqlClient.SqlParameter("column", dataObj.getFilestreamColumn()), 
                new Microsoft.Data.SqlClient.SqlParameter("table", dataObj.getTableName()), 
                new Microsoft.Data.SqlClient.SqlParameter("id", dataObj.getFileGUID())
            };
            rowInfo = context.Database.SqlQueryRaw<FileStreamRowInfo>("SELECT @column.PathName() AS 'path', GET_FILESTREAM_TRANSACTION_CONTEXT() AS 'transactionContext' " +
                                                                        "FROM @table " +
                                                                        "WHERE FileGUID = @id", parameters).First();
        }

        public Task<byte[]> GetFilestreamData() {
            //Code for this section developed by Daz Fuller: http://ignoringthevoices.blogspot.com/2014/01/working-with-entity-framework-code.html
            var buffer = new byte[16 * 1024];
            using (var handle = new SqlFileStream(rowInfo.path, rowInfo.transactionContext, FileAccess.Read)) {
                using (var ms = new MemoryStream()) {
                    int bytesRead;
                    while ((bytesRead = handle.Read(buffer, 0, buffer.Length)) > 0) {
                        ms.Write(buffer, 0, bytesRead);
                    }
                    return Task.FromResult(ms.ToArray());
                }
            }
        }

        public Task SetFilestreamData(byte[] data) {
            var buffer = new byte[16 * 1024];
            using (var handle = new SqlFileStream(rowInfo.path, rowInfo.transactionContext, FileAccess.Write))
            {
                using (var ms = new MemoryStream(data))
                {
                    int bytesRead;
                    while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        handle.Write(buffer, 0, bytesRead);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
