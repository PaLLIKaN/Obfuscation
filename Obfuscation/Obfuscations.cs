using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Обфускация.Class
{
    public class Obfuscations
    {
        private string kod; // Переменная хранящая текст выбранной программы

        public Obfuscations() { }


        public string Kod
        {
            get { return kod; }
            set { kod = value; }
        }

        public int Start_index(char a, char b, int i, int Start_comment) //Определяет начало комментария
        {
            if (kod[i - 1] == a && kod[i] == b && Start_comment == -1)
            {
                Start_comment = i - 1;
            }
            return Start_comment;
        }
        public void Deleting_comments()
        {
            int Start_comment = -1; // Определяет, где начинается комментарий и когда закрывается
            int Type_of_comment = 0; //Определяет тип комментария
            int Start_string = 0; //Переменныя улавливающее начало текста выводимого кодом программы и конец.
            for (int i = 1; i < kod.Length; i++)
            {
                if (Start_string == 0)
                {

                    //Удаление комментариев начинающихся с //
                    Start_comment = Start_index('/', '/', i, Start_comment);
                    if (Start_comment == i - 1 && Type_of_comment == 0)
                    {
                        Type_of_comment = 1;
                    }
                    if (Type_of_comment == 1)
                    {
                        if ((Start_comment >= 0 && kod[i] == '\n') || (Start_comment >= 0 && i + 1 == kod.Length))
                        {
                            kod = kod.Remove(Start_comment, i - Start_comment + 1);
                            i -= i - Start_comment;
                            Start_comment = -1;
                            Type_of_comment = 0;
                            continue;
                        }
                    }

                    //Удаление комментариев начинающихся с "/*"
                    Start_comment = Start_index('/', '*', i, Start_comment);
                    if (Start_comment == i - 1 && Type_of_comment == 0)
                    {
                        Type_of_comment = -1;
                    }
                    if (Type_of_comment == -1)
                    {
                        if (Start_comment >= 0 && kod[i - 1] == '*' && kod[i] == '/')
                        {
                            kod = kod.Remove(Start_comment, i - Start_comment + 1);
                            i -= i - Start_comment;
                            Start_comment = -1;
                            Type_of_comment = 0;
                            continue;
                        }
                    }
                    if (Start_comment == -1 && kod[i] == '"')
                    {
                        Start_string = 1;
                        continue;
                    }
                }
                else
                {
                    if (kod[i] == '"')
                    {
                        Start_string = 0;
                    }
                }
            }
        }

        public void Adding_false_comments()
        {
            List<string> comments = new List<string>() { " Подсчитываем сумму элементов массива", " Не трогай этот код, он все сломает!",
                " WTF?!?!", " Этот код написан идиотом, но он работает", " TODO: Исправить эту ошибку до следующего понедельника",
                " Проверяем, является ли число четным (при условии, что число 5 - четное)", "Эта строка - массив символов", "Сумма двух цифр очень важна",
                "Возможен выход за границу string", "Не трогать", "Уберешь все сломается!!!", "Этот код — дно, я знаю." ,
                "Класс, использующийся для обходного решения — Richard being a f***ing idiot", "Этот код сломан", "Эта кастыль",
                "Этот код ничего не делает", "Это решение я нашел в интернете", "Этот код самый быстрый способ умножить два числа",
                "Это мой код и я его написал", "Этот код должен работать", "Эта переменная не используется, ноя не удалю её чтобы сохранить баланс в коде",
                "Здесь я хотел убедиться, что все работает правильно", "Почему бы не объединить все массивы в один?", "Этот код рисует круг",
                "Это очень умный код, был бы ошеломлён, если бы он работал", "Этот алгоритм не работает, нужно исправлять",
                "Нужно использовать эту функцию, это сделает код более красивым", "Считываем байты всех пикселей фотографии",
                "Получаем строку которую необходимо поместить", "Длина текста", "Переменная символа строки", "Переменная бит представления символа строки",
                "цикл идущий по строке", "цикл идущий по одному пиксилю ", "помещаем в пиксиль букву",
                "получаем несжатое растровое изображение из представления байтов", "помещаем полученное фото в рамку", "Помещаем биты в",
                "Этот метод создан Кевином, создатель должен знать, что делает", "Здесь идёт проверка значений, для нахождения ошибок",
                "Эта функция может вызывать ошибки, но она никогда не вызывала",
                "Этот код работал хорошо во время тестирования проверить, что он всё ещё работает", "Здесь я опечатался, ещё раз исправляю ошибку",
                "Этот цикл работает на 100%", "Этот метод должен принимать строку, но на вход он получает число", "Hi! :)",
                "Эта переменная всегда будет положительной", "Эта функция обрабатывает телефонные номера и преобразует их в формат, пригодный для отправки SMS-сообщений. На вход функция получает номер, на выходе - готовый к отправке текстовый файл. Функция использует регулярные выражения и вызывает API-методы компании, обеспечивающей SMS-рассылки.",
                "Это не работает, но я не знаю, почему", "Версия 1.1, изменении 25 - исправлено оформление кода",
                "Эта переменная используется только один раз", "Это должно было быть решено в версии 2.1 но я не добрался до этого",
                "Получаем выбранные пользователем опции", "имя файла", "Запускаем главное окно", "Закрываем текущее окно", "Создаём новый экземпляр главного окна",
                "переключение на страницу определения Fuzzer", "переключение на страницу выбора Fuzzer",
                "Устанавливаем идентификатор пользователя и обновляем его на сервере", "Очепятка в слове Hello",
                "Этот код не используется с версии 2.0", "Привет, если ты это видишь, напиши мне сообщение!", "Метод, который возвращает сумму двух чисел",
                "Данный код написан глупым человеком", "Метод для получения данных с сервера, который больше не используется",
                "Этот блок кода можно закомментировать, чтобы ускорить работу", "Это должен быть if, но я случайно написал case",
                "TODO: Добавить проверку на валидность email-адреса"};
            int time = 0; //Переменныя регулирующая, как часто будут добавляться вредоносные коментарии
            int Start_string = 0; //Переменныя улавливающее начало текста выводимого кодом программы и конец.
            Random rnd = new Random();
            for (int i = 0; i < kod.Length; i++)
            {
                if (Start_string == 0)
                {
                    if (kod[i] == '"')
                    {
                        Start_string++;
                        continue;
                    }
                    if ((kod[i] == ';' || kod[i] == '{' || kod[i] == '}'))
                    {
                        time++;
                        if (time == 5)
                        {
                            int rand = rnd.Next();
                            time = 0;
                            kod = kod.Insert(i + 1, "/*" + comments[rand % 69] + "*/"); // Помещение рандомного комментария из базы
                        }
                    }
                }
                else
                {
                    if (kod[i] == '"')
                    {
                        Start_string = 0;
                    }
                }
            }
        }

        public void Deleting_extra_characters()
        {
            kod = kod.Replace('\n', ' ');
            kod = kod.Replace('\r', ' ');
            List<string> significant_characters = new List<string>() { ";", ":", "-", "/", "-=", "/=", "&&", "<", ">", "=", ">=", "<=", "==", "=>", "=<", "{", "}", "," }; //Возможные знаки перед которыми ставятся пробелы упрощающие чтение кода 
            foreach (string charecter in significant_characters)
            {
                kod = Regex.Replace(kod, " " + charecter, charecter);
                kod = Regex.Replace(kod, charecter + " ", charecter);
            }
            int Start_string = 0;
            for (int i = 1; i < kod.Length; i++)
            {
                if (Start_string == 0)
                {
                    if ((kod[i - 1] == ' ' && kod[i] == ' ') || (kod[i - 1] == ';' && kod[i] == ' ') || (kod[i - 1] == ',' && kod[i] == ' ')) // Удаление лишних пробелов
                    {
                        kod = kod.Remove(i, 1);
                        i--;
                    }
                    if (kod[i] == '"')
                    {
                        Start_string = 1;
                        continue;
                    }
                }
                else
                {
                    if (kod[i] == '"')
                    {
                        Start_string = 0;
                    }
                }
            }
        }
        public void Changing_the_name_of_variables()
        {
            List<string> variables = new List<string>() { "string", "int", "uint", "bool", "void", "short", "ushort", "long", "ulong", "float", "double", "char", "sbyte", "byte", "var", "class", @"string\?" }; // Лист все возможных типов данных
            string Lexic_book = "QqWwEeRrTtYyUuIiOoPpAaSsDdFfGgHhJjKkLlZzXxCcVvBbNnMm_1234567890"; // Строка содержащая все возможные символы для имен переменных
            List<string> New_names = new List<string>(); // Дист содержащий новые именования переменных
            string ID_after_the_change; // Название переменной до изменения
            List<string> ID_before_the_change = new List<string>(); // Название переменной после изменения
            List<string> Immutable_Names = new List<string>();

            Random rnd = new Random();
            foreach (string variable in variables)
            {
                string pattern = $@"{variable} \w+"; // Шаблон, улавлявающий все возможные инициализированные переменные в коде
                Regex rg = new Regex(pattern);
                MatchCollection matchedAuthors = rg.Matches(kod);

                /// Выводим всех подходящих авторов  
                for (int count = 0; count < matchedAuthors.Count; count++)
                {

                    ID_before_the_change.Add(Regex.Replace(matchedAuthors[count].Value, variable + " ", ""));
                    New_names.Add(Regex.Replace(matchedAuthors[count].Value, variable + " ", ""));
                }

                string proverka = $@"override {variable} \w+"; // Шаблон улавливающий все имена которые нельзя изменять
                Regex Override = new Regex(proverka);
                MatchCollection matchOverride = Override.Matches(kod);

                for(int count = 0;count < matchOverride.Count; count++)
                {
                    Immutable_Names.Add(Regex.Replace(matchOverride[count].Value, $@"override {variable} ", ""));
                }
            }
            foreach (string Before in ID_before_the_change)
            {
                do
                {
                    StringBuilder variable_name = new StringBuilder(Before); // Формирование нового имени переменной
                    for (int count2 = 0; count2 < Before.Length; count2++)
                    {
                        int character_exchange = rnd.Next(1, 37);
                        if (count2 == 0)
                        {
                            variable_name[count2] = Lexic_book[character_exchange % 53];
                        }
                        else
                        {
                            variable_name[count2] = Lexic_book[character_exchange];
                        }
                    }
                    ID_after_the_change = variable_name.ToString();

                } while (New_names.Contains(ID_after_the_change)); // Запрет на одинаковое название переменных
                New_names.Add(ID_after_the_change);

                if (Before != "operator" && !Immutable_Names.Contains(Before))
                {
                    kod = Regex.Replace(kod, $@"\b{Before}\b", ID_after_the_change);
                }

            }
        }
    }
}
