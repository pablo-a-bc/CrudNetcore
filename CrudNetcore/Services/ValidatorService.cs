using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace ApiAgendamientoWeb.Servicios
{
	public class ValidatorService
	{

		public bool IsValidEmail(string email)
		{
			bool response = new bool();
			try
			{
				var addr = new MailAddress(email);
				response = addr.Equals(email);
				return response;
			}
			catch (Exception e)
			{

				return response;
			}
		}

		/*
		 * Método que valida el rut con el digito verificador por separado
		 */
		public bool ValidaRut(string rut, string dv)
		{
			return ValidaRut(rut + "-" + dv);
		}

		/*
		 * Metodo de validación de rut con digito verificador dentro de la cadena (ej: 1-9)
		 */
		public bool ValidaRut(string rut)
		{
			try
			{
				rut = rut.Replace(".", "").ToUpper();
				Regex expresion = new Regex("^([0-9]+-[0-9K])$");
				string dv = rut.Substring(rut.Length - 1, 1);
				if (!expresion.IsMatch(rut))
				{
					return false;
				}
				char[] charCorte = { '-' };
				string[] rutTemp = rut.Split(charCorte);
				if (dv != Digito(int.Parse(rutTemp[0])))
				{
					return false;
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		/*
		 * método que calcula el digito verificador a partir de la mantisa del rut
		 */
		public static string Digito(int rut)
		{
			int suma = 0;
			int multiplicador = 1;
			while (rut != 0)
			{
				multiplicador++;
				if (multiplicador == 8)
					multiplicador = 2;
				suma += (rut % 10) * multiplicador;
				rut /= 10;
			}
			suma = 11 - (suma % 11);
			if (suma == 11)
			{
				return "0";
			}
			else if (suma == 10)
			{
				return "K";
			}
			else
			{
				return suma.ToString();
			}
		}


		public string Encriptar(string password)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}

		}
	}
}
