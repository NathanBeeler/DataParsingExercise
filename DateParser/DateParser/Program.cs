using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DateParser
{
  class Program
  {
    //Assuming US culture for date parsing
    static readonly CultureInfo us = new CultureInfo("en-US");

    static void Main(string[] args)
    {
      try
      {
        //Assuming a smallish data file that allows pulling it all in to memory at once instead of taking on the overhead and
        //repeated file access of reading it one character at a time.
        string txt = File.ReadAllText(@"Data\MarketingDataFile.txt");

        //Using a queue to let .NET handle FIFO operations for each new digit
        Queue<char> testDate = new Queue<char>();
        string validDate = "";

        //Loop through each character in the data, add digits to the queue, and check each 8 character batch to see if it's a valid past date
        foreach (char d in txt)
        {
          if (char.IsDigit(d))
          {
            testDate.Enqueue(d);

            if (testDate.Count == 8 && TestArrayForPastDate(testDate.ToArray(), out validDate))
            {
              Console.WriteLine(validDate);
            }

            if (testDate.Count == 8)
              testDate.Dequeue();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("The data file could not be found:");
        Console.WriteLine(e.Message);
      }
      Console.ReadKey();
    }

    public static bool TestArrayForPastDate(char[] testDate, out string dt)
    {
      //Capture zero based month before adding offset, so it can be sent to the output
      dt = new string(testDate);

      //Adding offset to month before test in native char, rather than converting back and forth to int, ala --
      //int month = Convert.ToInt32(Char.GetNumericValue(testDate[0]) * 10 + Char.GetNumericValue(testDate[1])) + 1; //Add 1 for the offset
      if (testDate[0] == '0' && testDate[1] == '9')
      {
        testDate[0] = '1';
        testDate[1] = '0';
      }
      else if (testDate[1] == '9')
      {
        return false;
      }
      else
      {
        testDate[1]++;
      }

      DateTime validDate = new DateTime();
      return (DateTime.TryParseExact(new string(testDate), "MMddyyyy", us, DateTimeStyles.None, out validDate)
                        && validDate.Date < DateTime.Today.Date);
    }
  }
}
