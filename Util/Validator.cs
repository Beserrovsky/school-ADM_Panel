﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FelipeB_App3BI.Util
{
    public class Validator
    {
        public static bool CheckCpf(string cpf)
        {

            if (cpf == null || cpf.Length != 11) return false; // Verifica se Array está mal formatado

            int[] cpf_arr = new int[11];
            for (int i = 0; i < cpf.Length; i++)
                if (!int.TryParse(cpf[i].ToString(), out cpf_arr[i])) return false; // Converte de String para int[] e retorna falso caso não seja um número

            int sum = 0;

            for (int i = 10; i >= 2; i--)
            { // Multiplica os 9 primeiros números pela seqência decrescente de 10 a 2
                sum += cpf_arr[10 - i] * i;
            }

            double mod = (sum * 10) % 11;

            if (mod != cpf_arr[9]) return false; // Verfica se o resto da soma anterior quando multipliacada por 10 e dividida por 11 é igual ao primeiro digito da confirmação

            sum = 0;

            for (int i = 11; i >= 2; i--)
            { // Multiplica os 10 primeiros números pela seqência decrescente de 11 a 2
                sum += cpf_arr[11 - i] * i;
            }

            mod = (sum * 10) % 11;

            if (mod != cpf_arr[10]) return false; // Repete a etapa de verificação, mas para o segundo digito

            return true;
        }
    }
}