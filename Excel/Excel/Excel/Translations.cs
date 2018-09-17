using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    static class Translations
    {

        private static bool CanIAddOperationToStack(string newOperator, string stackOperator)
        {
            if ((stackOperator == "+" || stackOperator == "-")
                    && (newOperator == "*"
                        || newOperator == "/"))

            {
                return true;
            }
            else if (stackOperator == "")
            {
                return true;
            }

            return false;
        }


        public static List<string> TranslationsToONP(List<string> operation)
        {
            List<string> ONPList = new List<string>();
            Stack stack = new Stack();
            string elementOnStack = "";

            foreach (var element in operation)
            {
                if (element != "*" && element != "/" && element != "+" && element != "-")
                {
                    ONPList.Add(element);

                }
                else
                {

                    while (CanIAddOperationToStack(element, elementOnStack) == false)
                    {
                        string elemenFromTheTop = stack.Pop().ToString();

                        ONPList.Add(elemenFromTheTop);
                        if (stack.Count == 0)
                        {
                            elementOnStack = "";
                        }
                        else
                        {
                            elementOnStack = stack.Peek().ToString();
                        }
                    }

                    if (CanIAddOperationToStack(element, elementOnStack) == true)
                    {
                        stack.Push(element);
                        elementOnStack = stack.Peek().ToString();
                    }

                }
            }

            while (stack.Count > 0)
            {
                string addOperationToONPList = stack.Pop().ToString();
                ONPList.Add(addOperationToONPList);
            }
            stack.Clear();

            return ONPList;
        }



    }
}
