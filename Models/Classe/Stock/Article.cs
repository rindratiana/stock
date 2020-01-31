using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

namespace stock.Models.Classe.Stock
{
    public class Article
    {
        int id;
        string references;
        string designation;
        string code;
        string emplacement;

        public Article(int id, string references, string designation, string code, string emplacement)
        {
            this.Id = id;
            this.References = references;
            this.Designation = designation;
            this.Code = code;
            this.Emplacement = emplacement;
        }

        public Article()
        {
        }
        public List<Article> GetListeArticleExcel2()
        {
            List<Article> reponse = new List<Article>();
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"E:\test.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    //new line
                    if (j == 1)
                        Console.Write("\r\n");

                    //write the value to the console
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null) { 
                        Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                    Article tmp = new Article();
                    tmp.Designation = xlRange.Cells[i, j].Value2.ToString();
                    reponse.Add(tmp);
                    }
                }
            }
            return reponse;

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }
        public List<Article> GetListeArticleExcel(HttpPostedFileBase files)
        {
            List<Article> reponse = new List<Article>();
            try
            {
                string filePath = @"E:\test.xlsx";
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
                OleDbConnection connection = new OleDbConnection(connectionString);
                string cmdText = "SELECT * FROM [Feuil1$]";
                OleDbCommand command = new OleDbCommand(cmdText, connection);

                command.Connection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                Article temp = new Article();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Console.WriteLine("{0}\t{1}", reader[0].ToString(), reader[1].ToString());
                        temp.designation = reader[0].ToString();
                        reponse.Add(temp);

                    }
                }
                return reponse;
            }
            catch (Exception exception) {
                throw new Exception(exception.Message);
            }
        }
        public int Id { get => id; set => id = value; }
        public string References { get => references; set => references = value; }
        public string Designation { get => designation; set => designation = value; }
        public string Code { get => code; set => code = value; }
        public string Emplacement { get => emplacement; set => emplacement = value; }
    }
}