using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    class Calculations
    {
        public static int CalculateTheValue(List<string> ONPList)
        {
            Stack stackWithNumber = new Stack();

            foreach (var element in ONPList)
            {

                if (element != "*" && element != "/" && element != "+" && element != "-")
                {
                    stackWithNumber.Push(element);
                }
                else
                {
                    string firstElementOnStack = stackWithNumber.Pop().ToString();
                    string secondElementOnStack = stackWithNumber.Pop().ToString();

                    int valueFirstElementOnStack = int.Parse(firstElementOnStack);
                    int valueSecondElementOnStack = int.Parse(secondElementOnStack);
                    if (element == "+")
                    {
                        int value = valueFirstElementOnStack + valueSecondElementOnStack;
                        stackWithNumber.Push(value);
                    }
                    else if (element == "-")
                    {
                        int value = valueSecondElementOnStack - valueFirstElementOnStack;
                        stackWithNumber.Push(value);
                    }
                    else if (element == "*")
                    {
                        int value = valueFirstElementOnStack * valueSecondElementOnStack;
                        stackWithNumber.Push(value);
                    }
                    else
                    {
                        int value = valueSecondElementOnStack / valueFirstElementOnStack;
                        stackWithNumber.Push(value);
                    }
                }

            }
            string result = stackWithNumber.Pop().ToString();
            int finalResult = int.Parse(result);
            return finalResult;

        }
    }
}
