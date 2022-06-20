using System;
using System.Globalization;

namespace ConCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                string inp = Console.ReadLine();
                Console.WriteLine("  " + Calc(Prepare(inp)));
            }
        }
        private static string Prepare(string f)
        {
            string o = "";
            bool lastIsOp = true;
            for(int i=0; i<f.Length; i++)
            {
                if (lastIsOp && f[i] == '-')
                {
                    o += '_';
                }
                else if (!lastIsOp && f[i] == '(')
                {
                    o += "*(";
                }
                else if(f[i] != ' ')
                {
                    o += f[i];
                }

                if ("+-*/^".Contains(f[i]))
                {
                    lastIsOp = true;
                }
                else if ("0123456789._".Contains(f[i]))
                {
                    lastIsOp = false;
                }
            }
            return o;
        }
        private static string Calc(string f)
        {
            while(f.Contains('(') && f.Contains(')'))
            {
                f = f.Substring(0, f.IndexOf('(')) + Calc(f[(f.IndexOf('(') + 1) .. f.LastIndexOf(')')]) +
                    f.Substring(f.LastIndexOf(')') + 1);
            }

            while (f.Contains('^'))
            {
                f = Mod(f);
            }
            while (f.Contains('*') || f.Contains('/'))
            {
                if(f.IndexOf('*') >= 0 && (f.IndexOf('*') < f.IndexOf('/') || f.IndexOf('/') < 0))
                {
                    f = Mult(f);
                }
                else
                {
                    f = Div(f);
                }
            }
            while (f.Contains('+') || f.Contains('-'))
            {
                if (f.IndexOf('+') >= 0 && (f.IndexOf('+') < f.IndexOf('-') || f.IndexOf('-') < 0))
                {
                    f = Add(f);
                }
                else
                {
                    f = Sub(f);
                }
            }

            return f.Replace('_', '-');
        }
        private static string Mult(string s)
        {
            if (!s.Contains('*')) return s;

            s = s.Replace(" ", "");
            s = s.Replace(",", ".");

            int opi = s.IndexOf('*');
            string nr0 = "";
            string nr1 = "";
            for(int i=opi-1; i>=0 && "1234567890._".Contains(s[i]); i--)
            {
                nr0 = s[i] + nr0;
            }
            for (int i = opi + 1; i < s.Length && "1234567890._".Contains(s[i]); i++)
            {
                nr1 += s[i];
            }

            nr0 = nr0.Replace('_', '-');
            nr1 = nr1.Replace('_', '-');
            string res = DoubleToString(double.Parse(nr0, CultureInfo.InvariantCulture) * double.Parse(nr1, CultureInfo.InvariantCulture))
                .Replace('-', '_');

            return s.Substring(0, opi - nr0.Length) + res + s.Substring(opi + 1 + nr1.Length);
        }
        private static string Mod(string s)
        {
            if (!s.Contains('^')) return s;

            s = s.Replace(" ", "");
            s = s.Replace(",", ".");

            int opi = s.IndexOf('^');
            string nr0 = "";
            string nr1 = "";
            for (int i = opi - 1; i >= 0 && "1234567890._".Contains(s[i]); i--)
            {
                nr0 = s[i] + nr0;
            }
            for (int i = opi + 1; i < s.Length && "1234567890._".Contains(s[i]); i++)
            {
                nr1 += s[i];
            }

            nr0 = nr0.Replace('_', '-');
            nr1 = nr1.Replace('_', '-');
            string res = DoubleToString(Math.Pow(double.Parse(nr0, CultureInfo.InvariantCulture), double.Parse(nr1, 
                CultureInfo.InvariantCulture))).Replace('-', '_');

            return s.Substring(0, opi - nr0.Length) + res + s.Substring(opi + 1 + nr1.Length);
        }
        private static string Div(string s)
        {
            if (!s.Contains('/')) return s;

            s = s.Replace(" ", "");
            s = s.Replace(",", ".");

            int opi = s.IndexOf('/');
            string nr0 = "";
            string nr1 = "";
            for (int i = opi - 1; i >= 0 && "1234567890._".Contains(s[i]); i--)
            {
                nr0 = s[i] + nr0;
            }
            for (int i = opi + 1; i < s.Length && "1234567890._".Contains(s[i]); i++)
            {
                nr1 += s[i];
            }

            nr0 = nr0.Replace('_', '-');
            nr1 = nr1.Replace('_', '-');
            string res = DoubleToString(double.Parse(nr0, CultureInfo.InvariantCulture) / double.Parse(nr1, CultureInfo.InvariantCulture))
                .Replace('-', '_');

            return s.Substring(0, opi - nr0.Length) + res + s.Substring(opi + 1 + nr1.Length);
        }
        private static string Add(string s)
        {
            if (!s.Contains('+')) return s;

            s = s.Replace(" ", "");
            s = s.Replace(",", ".");

            int opi = s.IndexOf('+');
            string nr0 = "";
            string nr1 = "";
            for (int i = opi - 1; i >= 0 && "1234567890._".Contains(s[i]); i--)
            {
                nr0 = s[i] + nr0;
            }
            for (int i = opi + 1; i < s.Length && "1234567890._".Contains(s[i]); i++)
            {
                nr1 += s[i];
            }

            nr0 = nr0.Replace('_', '-');
            nr1 = nr1.Replace('_', '-');
            string res = DoubleToString(double.Parse(nr0, CultureInfo.InvariantCulture) + double.Parse(nr1, CultureInfo.InvariantCulture))
                .Replace('-', '_');

            return s.Substring(0, opi - nr0.Length) + res + s.Substring(opi + 1 + nr1.Length);
        }
        private static string Sub(string s)
        {
            if (!s.Contains('-')) return s;

            s = s.Replace(" ", "");
            s = s.Replace(",", ".");

            int opi = s.IndexOf('-');
            string nr0 = "";
            string nr1 = "";
            for (int i = opi - 1; i >= 0 && "1234567890._".Contains(s[i]); i--)
            {
                nr0 = s[i] + nr0;
            }
            for (int i = opi + 1; i < s.Length && "1234567890._".Contains(s[i]); i++)
            {
                nr1 += s[i];
            }

            nr0 = nr0.Replace('_', '-');
            nr1 = nr1.Replace('_', '-');
            string res = DoubleToString(double.Parse(nr0, CultureInfo.InvariantCulture) - double.Parse(nr1, CultureInfo.InvariantCulture))
                .Replace('-', '_');

            return s.Substring(0, opi - nr0.Length) + res + s.Substring(opi + 1 + nr1.Length);
        }
        private static string DoubleToString(double d)
        {
            // from https://codereview.stackexchange.com/q/149382

            string R = d.ToString("R", CultureInfo.InvariantCulture).Replace(",", "");
            int i = R.IndexOf('E');

            if (i < 0)
                return R;

            string G17 = d.ToString("G17", CultureInfo.InvariantCulture);

            if (!G17.Contains('E'))
                return G17.Replace(",", "");

            // manual parsing
            string beforeTheE = R.Substring(0, i);
            int E = Convert.ToInt32(R.Substring(i + 1));

            i = beforeTheE.IndexOf('.');

            if (i < 0)
                i = beforeTheE.Length;
            else
                beforeTheE = beforeTheE.Replace(".", "");

            i += E;

            while (i < 1)
            {
                beforeTheE = "0" + beforeTheE;
                i++;
            }

            while (i > beforeTheE.Length)
            {
                beforeTheE += "0";
            }

            if (i == beforeTheE.Length)
                return beforeTheE;

            return String.Format("{0}.{1}", beforeTheE.Substring(0, i), beforeTheE.Substring(i));
        }
    }
}
