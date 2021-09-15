using System;

namespace FelipeB_App3BI.Util
{
    public class Masker
    {

        public static string MaskCpf(string cpf)
        {
            if (cpf == null) return "BAD FORMATTED STRING! (null)";
            if (cpf.Length != 11) return "BAD FORMATTED STRING! (length)";

            return $"{cpf[0]}{cpf[1]}{cpf[2]}.{cpf[3]}{cpf[4]}{cpf[5]}.{cpf[6]}{cpf[7]}{cpf[8]}-{cpf[9]}{cpf[10]}";
        }

        public static string MaskTelephone(string tel)
        {
            if (tel == null) return "BAD FORMATTED STRING! (null)";
            switch (tel.Length)
            {
                case 10:
                    return $"({tel[0]}{tel[1]}) {tel[2]}{tel[3]}{tel[4]}{tel[5]}-{tel[6]}{tel[7]}{tel[8]}{tel[9]}";
                case 11:
                    return $"({tel[0]}{tel[1]}) {tel[2]}{tel[3]}{tel[4]}{tel[5]}{tel[6]}-{tel[7]}{tel[8]}{tel[9]}{tel[10]}";
                default:
                    return "BAD FORMATTED STRING! (length)";
            }
        }
    }
}