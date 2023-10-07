namespace Util
{
    public static class Input
    {
        /// <summary>
        /// Takes input until the validator function returns a valid T. Then outputs it to the result out parameter and returns.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prompt">Initial prompt to write to the output before taking input</param>
        /// <param name="result">Output parameter, its value after calling the function will either be the successfully validated input, or a default value of the type if it failed.</param>
        /// <param name="input">Text input to read from</param>
        /// <param name="output">Text output to write prompts and messages to</param>
        /// <param name="validator">A delegate that will take the line of input and return a tuple containing a T? (which is null if the input is invalid, otherwise it contains the parsed and validated value that will be used in the out parameter), a string (which is the message to display if the input was invalid), and a bool (which is true if you want the prompt to be written again).</param>
        /// <returns>True if the read succeeded, false if the read failed</returns>
        public static bool ValidatedInput<T>(string prompt, out T result, System.IO.TextReader input, System.IO.TextWriter output, Func<string, (Nullable<T>, string, bool)> validator) where T: struct
        {
            result = default;
            Nullable<T> validatorResult = null;
            string? message = null;
            bool shouldPrompt = true;

            while (validatorResult == null)
            {
                if (message != null)
                    Console.WriteLine(message);
                if (shouldPrompt)
                    output.Write(prompt);

                string? userInput = input.ReadLine();
                if (userInput == null) return false;
                (validatorResult, message, shouldPrompt) = validator(userInput);
            }
            result = (T)validatorResult;
            return true;
        }

        /// <summary>
        /// Takes input until the validator function returns a valid T. Then outputs it to the result out parameter and returns.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prompt">Initial prompt to write to the output before taking input</param>
        /// <param name="result">Output parameter, its value after calling the function will either be the successfully validated input, or a default value of the type if it failed.</param>
        /// <param name="validator">A delegate that will take the line of input and return a tuple containing a T? (which is null if the input is invalid, otherwise it contains the parsed and validated value that will be used in the out parameter), a string (which is the message to display if the input was invalid), and a bool (which is true if you want the prompt to be written again).</param>
        /// <returns>True if the read succeeded, false if the read failed</returns>
        public static bool ValidatedInput<T>(string prompt, out T result, Func<string, (Nullable<T>, string, bool)> validator) where T : struct
        {
            return ValidatedInput<T>(prompt, out result, Console.In, Console.Out, validator);
        }
        /// <summary>
        /// Takes input until the validator function returns a valid T. Then outputs it to the result out parameter and returns.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prompt">Initial prompt to write to the output before taking input</param>
        /// <param name="result">Output parameter, its value after calling the function will either be the successfully validated input, or a default value of the type if it failed.</param>
        /// <param name="input">Text input to read from</param>
        /// <param name="output">Text output to write prompts and messages to</param>
        /// <param name="validator">A delegate that will take the line of input and return a tuple containing a T? (which is null if the input is invalid, otherwise it contains the parsed and validated value that will be used in the out parameter), a string (which is the message to display if the input was invalid), and a bool (which is true if you want the prompt to be written again).</param>
        /// <returns>True if the read succeeded, false if the read failed</returns>
        public static bool ValidatedInput<T>(string prompt, out T? result, System.IO.TextReader input, System.IO.TextWriter output, Func<string, (T?, string, bool)> validator) where T : class
        {
            result = default;
            T? validatorResult = null;
            string? message = null;
            bool shouldPrompt = true;

            while (validatorResult == null)
            {
                if (message != null)
                    Console.WriteLine(message);
                if (shouldPrompt)
                    output.Write(prompt);

                string? userInput = input.ReadLine();
                if (userInput == null) return false;
                (validatorResult, message, shouldPrompt) = validator(userInput);
            }
            result = (T)validatorResult;
            return true;
        }

        /// <summary>
        /// Takes input until the validator function returns a valid T. Then outputs it to the result out parameter and returns.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prompt">Initial prompt to write to the output before taking input</param>
        /// <param name="result">Output parameter, its value after calling the function will either be the successfully validated input, or a default value of the type if it failed.</param>
        /// <param name="validator">A delegate that will take the line of input and return a tuple containing a T? (which is null if the input is invalid, otherwise it contains the parsed and validated value that will be used in the out parameter), a string (which is the message to display if the input was invalid), and a bool (which is true if you want the prompt to be written again).</param>
        /// <returns>True if the read succeeded, false if the read failed</returns>
        public static bool ValidatedInput<T>(string prompt, out T? result, Func<string, (T?, string, bool)> validator) where T : class
        {
            return ValidatedInput<T>(prompt, out result, Console.In, Console.Out, validator);
        }

    }
}