﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CapitalizationTool
{
    class CapitalizationTool
    {
        private Char[] punctuationMarks = { ';', ':', '.', ',', '!', '?', '-' };
        private String[] emptyWords = { "A", "An", "And", "At", "But", "By", "For", "In", "Nor", "Not",
            "Of", "On", "Or", "Out", "So", "The", "To", "Up", "Yet"};
        private Boolean IsEmptyWords(String line)
        {
            Boolean flag = false;
            for (UInt16 i = 0; i < emptyWords.Length; i++)
            {
                if (line == emptyWords[i])
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        private Boolean IsPunctuationMarks(Char mark)
        {
            Boolean flag = false;
            for (UInt16 i = 0; i < punctuationMarks.Length; i++)
            {
                if (mark == punctuationMarks[i])
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        public void Сapitalize()
        {
            UInt16 topCursorPosition = 0;
            String line;
            do
            {
                topCursorPosition++;
                Console.WriteLine("Enter title to capitalize: ");
                Console.ForegroundColor = ConsoleColor.Red;
                do
                {
                    Console.SetCursorPosition("Enter title to capitalize: ".Length, topCursorPosition - 1);
                    line = Console.ReadLine();
                    // строка обрабатывается, если она неулевой длины
                    if (line.Length != 0)
                    {
                        line = line.ToLower();
                        // если по ходу строки встречаются знаки препинания, отделяем их пробелами
                        for (Int16 i = 0; i < line.Length; i++)
                        {
                            if (IsPunctuationMarks(line[i]))
                            {
                                // откат индекса на 1, чтобы в Insert'ах не было выражений с вычитанием, тогда не работает
                                i--;
                                // первый пробел вставляется в позицию 1 от нового i
                                const UInt16 offsetIndexBeforePunctuationMark = 1;
                                // соответственно знак препинания будет в позиции 2,
                                // а пробел после него - в позиции 3
                                const UInt16 offsetIndexAfterPunctuationMark = 3;
                                // исходя из этого, следующее i встанет в позицию 4
                                const Int16 offsetIndexAfterSecondSpace = 4;
                                if (i >= -1)
                                {
                                    line = line.Insert(i + offsetIndexBeforePunctuationMark, " ");
                                }
                                line = line.Insert(i + offsetIndexAfterPunctuationMark, " ");
                                i += offsetIndexAfterSecondSpace;
                            }
                        }
                        // делаем из строки массив подстрок
                        String[] line1 = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        // поднимаем первые буквы подстрок
                        for (UInt16 i = 0; i < line1.Length; i++)
                        {
                            Char firstLetter = Char.ToUpper(line1[i][0]);
                            line1[i] = line1[i].Remove(0, 1);
                            line1[i] = line1[i].Insert(0, new String(firstLetter, 1));
                            //если текущее слово не первое и не последнее и является вспомогательным, опускаем первую букву
                            if (i != 0 && i != line1.Length - 1 && IsEmptyWords(line1[i]))
                            {
                                line1[i] = line1[i].ToLower();
                            }
                        }
                        // если в конце строки знак препинания, поднимаем первую букву слова перед ним
                        if (line1.Length > 1 && IsPunctuationMarks(line1[line1.Length - 1][0]))
                        {
                            Char firstLetter = Char.ToUpper(line1[line1.Length - 2][0]);
                            line1[line1.Length - 2] = line1[line1.Length - 2].Remove(0, 1);
                            line1[line1.Length - 2] = line1[line1.Length - 2].Insert(0, new String(firstLetter, 1));
                        }
                        // собираем строку
                        line = String.Join(" ", line1);
                        // если была введена пробельная строка, StringSplitOptions.RemoveEmptyEntries сделает ее пустой и она не будет обрабатываться, поэтому добавляем пробел
                        if (line.Length == 0)
                        {
                            line = line.Insert(0, " ");
                        }
                        // убираем пробел перед знаком препинания, если это не тире
                        for (UInt16 i = 1; i < line.Length; i++)
                        {
                            if (IsPunctuationMarks(line[i]) && line[i - 1] == ' ' && line[i] != '-')
                            {
                                line = line.Remove(i - 1, 1);
                            }
                        }
                    }
                }
                while (line.Length == 0);
                Console.ResetColor();
                Console.WriteLine("Capitalized title: ");
                Console.SetCursorPosition("Capitalized title: ".Length, topCursorPosition);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(line);
                Console.ResetColor();
                Console.WriteLine(" ");
                topCursorPosition+=2;
                }
            while (true);
        }
        public static void Main()
        {
            CapitalizationTool capitalizationTool = new CapitalizationTool();
            capitalizationTool.Сapitalize();
        }
    }
}