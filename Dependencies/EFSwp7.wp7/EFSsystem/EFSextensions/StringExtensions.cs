using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using JeffWilcox.Utilities.Silverlight;
using System.Diagnostics;

namespace EFSresources.EFSextensions
{
	public static class StringExtensions
	{
		public static string getMD5(this string input)
        {
			return MD5CryptoServiceProvider.GetMd5String(input);
        }

		//public static string DeleteHiddenCharacters(this string input)
		//{
			
		//}

		/// <summary>
		/// Returns a string with backslashes before characters that need to be quoted
		/// </summary>
		/// <param name="InputTxt">Text string need to be escape with slashes</param>
		public static string AddSlashes(this string InputTxt)
		{
			// List of characters handled:
			// \000 null
			// \010 backspace
			// \011 horizontal tab
			// \012 new line
			// \015 carriage return
			// \032 substitute
			// \042 double quote
			// \047 single quote
			// \134 backslash
			// \140 grave accent

			string Result = InputTxt;

			try
			{
				Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, @"[\000\010\011\012\015\032\042\047\134\140]", "\\$0");
			}
			catch (Exception Ex)
			{
				// handle any exception here
				Console.WriteLine(Ex.Message);
			}

			return Result;
		}

		/// <summary>
		/// Un-quotes a quoted string
		/// </summary>
		/// <param name="InputTxt">Text string need to be escape with slashes</param>
		public static string StripSlashes(this string InputTxt)
		{
			// List of characters handled:
			// \000 null
			// \010 backspace
			// \011 horizontal tab
			// \012 new line
			// \015 carriage return
			// \032 substitute
			// \042 double quote
			// \047 single quote
			// \134 backslash
			// \140 grave accent

			string Result = InputTxt;

			try
			{
				Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, @"(\\)([\000\010\011\012\015\032\042\047\134\140])", "$2");
			}
			catch (Exception Ex)
			{
				// handle any exception here
				Console.WriteLine(Ex.Message);
			}

			return Result;
		}

		public static string Remove65279(this string input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if ((int)input[i] == 65279)
				{
					input = input.Remove(i, 1);
					i--;
				}
			}

			return input;
		}
	}
}
