using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd
{
    public class Utils
    {
        private static string ToastSeparator = Configs.ToastSeparator;
        private static string cleanToastMessage(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input.Replace(ToastSeparator, " ");
        }


        public static string GenerateToastSuccess(string message, string title = "Success")
        {
            try
            {
                string finalMessage = $"success{ToastSeparator}{cleanToastMessage(message)}{ToastSeparator}{title}";
                return finalMessage;
            }
            catch (Exception e)
            {
                Utils.HandleException(e);
                return message;
            }
        }

        public static string GenerateToastInfo(string message, string title = "Message")
        {
            try
            {
                string finalMessage = $"info{ToastSeparator}{cleanToastMessage(message)}{ToastSeparator}{title}";
                return finalMessage;
            }
            catch (Exception e)
            {
                Utils.HandleException(e);
                return message;
            }
        }

        public static string GenerateToastError(string message = null, string title = "Error")
        {
            try
            {

                if (string.IsNullOrEmpty(message)) message = Configs.DefaultErrorMessage;

                string finalMessage = $"error{ToastSeparator}{cleanToastMessage(message)}{ToastSeparator}{title}";
                return finalMessage;
            }
            catch (Exception e)
            {
                Utils.HandleException(e);
                return message;
            }
        }

        public static void HandleException(Exception e)
        {

        }
    }
}