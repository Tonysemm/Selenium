using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Selenium
{
    public class TestCase
    {
        public int id_TestCase { get; set; }
        public bool ExpectedResult { get; set; }
        public bool ToExecute { get; set; }
        public int DatasetReference { get; set; }

    }
    public class PersonalData : TestCase
    {
        public int id { get; set; }
        public bool IsRefusedINN { get; set; }
        public bool IsReceivedINN { get; set; }
        public int INN { get; set; }
        public bool IsUKR { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public bool IsAbsentSecondName { get; set; }
        public string SecondName { get; set; }
        public int BirthDate { get; set; }
        public bool IsInsurerInsured { get; set; }
        public string KinshipTies { get; set; }
    }
    public class Documents : TestCase
    {
        public int id { get; set; }
        public string TypeOfDocument { get; set; }
        public string Series { get; set; }
        public int Number { get; set; }
        public int DateOfReceiving { get; set; }
        public string WhoGave { get; set; }
        public int ValidUntil { get; set; }
    }
    public static class DataSet
    {
        static Process[] procList;
        static DataSet()
        {
            procList = Process.GetProcessesByName("excel");
        }

        public static List<T> ReadExcel<T>() where T : class
        {
            //с помощью рефлексии определяем набор свойств принимаемого типа 
            Type t = typeof(T);
            object instance;
            PropertyInfo[] propertyInfos = t.GetProperties();

            List<T> result = new List<T>();



            //открываем Excel с данными
            Excel.Application xlApp = new Excel.Application();
            var b = Path(); 
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(Path() + @"\DataSet.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[t.Name];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            //определяем количество столбцов и рядов с заполненными данными
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;


            int count = 0;


            for (int j = 2; j <= colCount; j++)
            {
                //создаем экземпляр принятого типа
                instance = Activator.CreateInstance(t);
                foreach (var info in propertyInfos)
                {
                    for (int i = 1; i <= rowCount; i++)
                    {
                        //проверка на заполненность считуемой ячейки
                        if (xlRange.Cells[i, j].Value2 != null)
                        {
                            //если имя свойства совпадает с полем ячейки
                            if (info.Name == xlRange.Cells[i, 1].Value2.ToString())
                            {
                                //в зависимости от типа принимаемых данных, присваиваем значение соответствующему полю 
                                if (info.PropertyType == typeof(string))
                                {
                                    info.SetValue(instance, xlRange.Cells[i, j].Value2.ToString());
                                }
                                if (info.PropertyType == typeof(int))
                                {
                                    info.SetValue(instance, Convert.ToInt32(xlRange.Cells[i, j].Value2.ToString()));
                                }
                                if (info.PropertyType == typeof(bool))
                                {
                                    info.SetValue(instance, Convert.ToBoolean(xlRange.Cells[i, j].Value2.ToString()));
                                }
                            }
                        }
                        else
                        {
                            count += 1;
                            if (count == 2)
                            {
                                count = 0;
                                break;

                            }
                        }
                    }
                }
                //добавляем проинициализированный объект в список
                result.Add(instance as T);
            }

            //закрываем процесс
            Process[] newExcelList = Process.GetProcessesByName("excel");
                foreach (Process proc in newExcelList)
                {
                    if (procList.Where<Process>(x => x.Id == proc.Id).Count<Process>() == 0)
                    {
                        try
                        {
                            proc.Kill();
                        }
                        catch (Exception e) 
                        {
                            //throw e;
                        }
                    }
                }
            //возвращаем список объектов
            return result;
        }

        //определяем относительный путь к файлу с данными DataSet.xlsx
        private static string Path()
        {
            string path = Environment.CurrentDirectory;
            string result = "";
            int count = 0;
            for (int i = path.Length - 1; i >= 0; i--)
            {
                if (count == 3)
                {
                    result += path[i];
                }
                else if (path[i] == '\\')
                {
                    count++;
                }
            }
            var charArr = result.ToCharArray();
            result = "";
            Array.Reverse(charArr);
            foreach (var item in charArr)
            {
                result += item;
            };
            return result + @"\files\";
        }
    }
}
