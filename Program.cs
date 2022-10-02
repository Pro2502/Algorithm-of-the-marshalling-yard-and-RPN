using System;
using System.Collections.Generic;


namespace Сортировочная_станция
{
    class Program
    {
        static void Foo(ref int a, ref int b)
        {
            // данный параметр будет передоваться уже по ссылке, а не по значению(уже не будем копировать данные из одной переменной в другую)
            
        }
        static void Main(string[] args)
        {
            int variable;
            string s;// вписываем строку
            Stack<string> st = new Stack<string>();
            List<string> postfixNotation = new List<string>();
            List<string> operations = new List<string> { "+", "-", "/", "*", "^" };
            int last_stack_element = 0;
            int input_sequence_element = 0;

            Console.WriteLine("Впишите выражение, которое хотели преобразовать в ОПЗ и вычислить, с пробелами");
            s = Console.ReadLine();
            string[] subs = s.Split(' ');

            foreach (var sub in subs) // создаем результирующую последовательность
            {
                bool result = int.TryParse(sub, out variable); // условие на переменную

                if (result)
                {
                    string a = variable.ToString();
                    postfixNotation.Add(a);

                }
                else
                {
                    // проверяем верхний элемент стэка на приоритетность
                    if (sub != "(" && sub != ")")
                    {
                        for (int i = 0; i < operations.Count; i++)
                        {
                            if (st.Count>0 && operations[i] == st.Peek())
                            {
                                last_stack_element = i;
                                Foo(ref last_stack_element, ref input_sequence_element);
                            }
                            if (operations[i] == sub)
                            {
                                input_sequence_element = i;
                                Foo(ref last_stack_element, ref input_sequence_element);
                            }
                        }
                        if (last_stack_element > input_sequence_element)
                        {
                            postfixNotation.Add(st.Peek());
                        }
                        st.Push(sub);
                    }
                    if (sub == "(")
                    {
                        st.Push(sub);
                    }
                    if (sub == ")")
                    {
                        while (st.Peek() != "(")
                        {
                            postfixNotation.Add(st.Pop());
                            
                        }
                        if (st.Peek() == "(")
                        {
                            st.Pop();
                        }
                        else
                        {
                            Console.WriteLine("Ошибка в выражении! Не найдена открывающая скобка!");
                        }
                    }
                }
            }
            while (st.Count > 0)
            {
                    postfixNotation.Add(st.Pop());
            }
            
                string str = String.Join(' ', postfixNotation); 
                Console.WriteLine("Получена строка Обратной Польской Записи с пробелами:"+ str);

                double Variable;
                double operand;
                double lastnumb;
                string[] Subs = str.Split(' ');
                Stack<double> St = new Stack<double>();
                foreach (var Sub in Subs) // создаем стэк из преременных и выполняем вычисления
                {
                    bool Result = double.TryParse(Sub, out Variable); // условие на переменную
                    if (Result)
                    {
                        St.Push(Variable);
                    }
                    else
                    {
                        switch (Sub)
                        {
                            case "+":
                                St.Push(St.Pop() + St.Pop());
                                break;
                            case "*":
                                St.Push(St.Pop() * St.Pop());
                                break;
                            case "-":
                                operand = St.Pop();
                                St.Push(St.Pop() - operand);
                                break;
                            case "/":
                                operand = St.Pop();
                                if (operand != 0.0)
                                    St.Push(St.Pop() / operand);
                                else
                                    Console.WriteLine(" На ноль не делится! Ошибка!");
                                break;
                            case "^":
                                operand = St.Pop();
                                int i = Convert.ToInt32(operand);
                                operand = St.Pop();
                                int j = Convert.ToInt32(operand);
                                St.Push(j ^ i);
                                break;
                            default:
                                Console.WriteLine("Неправильная операция!");
                                break;
                        }
                    }
                }
                lastnumb = St.Pop();
                int end = Convert.ToInt32(lastnumb);
                Console.WriteLine("Вычисленное выражение равно " + end);
            
        }
    }
}
