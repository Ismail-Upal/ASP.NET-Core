using System.ComponentModel;

namespace ATMApp.UI;


public static class Validator
{
    public static T Convert<T>(string prompt)
    {
        bool valid = false;
        string userInput;
        while (!valid)
        {
            userInput = Utility.GetUserInput(prompt);
            try
            {
                var coverter = TypeDescriptor.GetConverter(typeof(T));
                if (coverter != null)
                {
                    return (T)coverter.ConvertFromString(userInput);
                }
                else
                {
                    return default;
                }
            }
            catch
            {
                Utility.PrintMessage("Invalid input. Try again.", false);
            }
        }
        return default;
    }
}
