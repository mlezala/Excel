using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using static System.Math;

namespace Excel
{
    class Program
    {
        static string[,] _excel = new string[100, 100];
        static List<string> _valuesList = new List<string>();
        static List<string> _operationsList = new List<string>();

        static void Main(string[] args)
        {
           
            string read2 = Read();
            string[] allRows = read2.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string[] excel = ParseRow(allRows);

            while (true)
            {
                string operation = GetOperation();

                if (operation.Contains("END"))
                {
                   break;
                }

                List<string> getElementOperationList = GetElementOperationList(operation);
                List<string> getElementOperationWithValuesList = GetElementOperationWithValuesList(getElementOperationList);
                List<string> ONPList = Translations.TranslationsToONP(getElementOperationWithValuesList);
                int value = Calculations.CalculateTheValue(ONPList);

                Console.WriteLine(value);              
            }
            

        }

 


        static string GetOperation()
        {
            Console.WriteLine("Enter the operation to be calculated, if you want to finish, enter END: ");

            string readOperation = Console.ReadLine();


            return readOperation;


        }

        static string Read()
        {

            Console.WriteLine("Enter your spreadsheet.");
            Console.WriteLine("1.Numbers separated by char | ");
            Console.WriteLine("2.End of the spreadsheet is ; ");

            string myString = "";
            while (true)
            {
                string readed = Console.ReadLine();

                if (readed.Contains(";"))
                {

                    myString += readed;
                    break;
                }
                else
                {
                    readed += "\r\n";
                    myString += readed;

                }
            }

            return myString;

        }

        static string ReadFile()
        {
            string allData = " ";

            try
            {
                using (StreamReader stream = new StreamReader(@"C:\Algorytmy\Excel\excel.txt"))
                {
                    allData = stream.ReadToEnd();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return allData;

        }

        static string[] ParseRow(string[] allRows)
        {
            int maxLenght = allRows.Length;
            string[] excel;
            excel = new string[100];

            for (int i = 0; i < maxLenght; i++)
            {
                excel = allRows[i].Split('|');

                PutRowToExcel(excel, i);
            }

            return excel;
        }

        static void PutRowToExcel(string[] excel, int row)
        {
            int maxLenght = excel.Length;
            for (int j = 0; j < maxLenght; j++)
            {
                _excel[row, j] = excel[j];
            }
        }


        static int GetValue(string position)
        {
            string rowS;
            string columnName;
            int row, value;

            columnName = position.Substring(0, 1);
            int columnIndex = Dictionary.MapLetterToInt(columnName);

            rowS = position.Substring(1, 1);
            row = int.Parse(rowS);

           

            if (_excel[row - 1, columnIndex - 1].Contains("+") ||
                (_excel[row - 1, columnIndex - 1].Contains("-") && !_excel[row - 1, columnIndex - 1].StartsWith("-")) ||
                _excel[row - 1, columnIndex - 1].Contains("*") || _excel[row - 1, columnIndex - 1].Contains("/"))
            {
                string operationInExcel = _excel[row - 1, columnIndex - 1];
                var getElementOperationList = GetElementOperationList(operationInExcel);
                var getElementOperationWithValuesList = GetElementOperationWithValuesList(getElementOperationList);
                List<string> ONPList = Translations.TranslationsToONP(getElementOperationWithValuesList);
                return value = Calculations.CalculateTheValue(ONPList);
            }
            else
            {              
                string clearValue = _excel[row - 1, columnIndex - 1].TrimEnd(';');
                return value = int.Parse(clearValue);
            }

        }

     


        static List<string> GetElementOperationList(string operation)
        {
            string number = "";
            
            List<string> elementsList = new List<string>();

            foreach (var element in operation)
            {

                if (element != '*' && element != '/' && element != '+' && element != '-')
                {
                    number += element;
                }
                else
                {
                    char operationChar = element;
                    elementsList.Add(number);
                    elementsList.Add(operationChar.ToString());
                    number = "";
                }
            }
            elementsList.Add(number);
            return elementsList;
        }

        static List<string> GetElementOperationWithValuesList(List<string> elementsList)
        {
            List<string> elementsWithValuesList = new List<string>();

            foreach (var element in elementsList)
            {

                if (element != "*" && element != "/" && element != "+" && element != "-")
                {
                    int value = GetValue(element);
                    elementsWithValuesList.Add(value.ToString());
                }
                else
                {

                    elementsWithValuesList.Add(element);

                }

            }
            return elementsWithValuesList;
        }

    }

}



