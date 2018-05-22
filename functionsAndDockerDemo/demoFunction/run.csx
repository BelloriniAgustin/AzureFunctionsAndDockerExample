#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static IActionResult Run(HttpRequest req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];
    log.Info("name = " + name);

    string message = req.Query["message"];
    log.Info("message = " + message);

    string numberOfShifts = req.Query["numberOfShifts"];
    log.Info("numberOfShifts = " + numberOfShifts);

    if (string.IsNullOrWhiteSpace(name)  || string.IsNullOrWhiteSpace(message)|| string.IsNullOrWhiteSpace(numberOfShifts) )
    {
        return new BadRequestObjectResult("Please pass a name, the message and the number of shifts to aply."); 
    } else {

         int shifts;
        try{

            shifts = Int32.Parse(numberOfShifts);

        } catch(FormatException exception){

            return new BadRequestObjectResult("Please pass a numeric digit in the number of shifts."); 

        }

      //  int shifts = Int32.Parse(numberOfShifts);


        return new OkObjectResult("Hello " + name + ", the message without cipher is: " + message + ", and the message with a Caesar's cipher with a shift of " + shifts + " is: " + Caesar(message,shifts)); 

    }

}

 public static string Caesar(string message, int shifts)
 {
        char[] buffer = message.ToCharArray();

        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];
            letter = (char)(letter + shifts);

            if (letter > 'z')
            {
                letter = (char)(letter - 26);
            }
            else if (letter < 'a')
            {
                letter = (char)(letter + 26);
            }
            buffer[i] = letter;
        }

        return new string(buffer);
}
