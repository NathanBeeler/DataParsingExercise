# DataParsingExercise
Data parsing exercise to pull valid calendar dates from a potentially corrupted file.

The DateParser assumes the following:
1) The dates the parser is looking for match US culture norms. 
2) The input file is not so large that it can't be ingested into a string internally. 
3) The output in console is in the same format that the input is: a date of an MMDDYYYY format with a zero-based month.

For this exercise I chose to keep things as simple and legible as I could, and there are notes within the code that show 
some decisions I made toward that end. There's a bit of overhead around dealing with the zero-based month, but I was 
unable to find a way to handle that natively in C# .NET (it looks like Java may handle this, but I'm not a Java programmer).
Additionally, I chose to pull characters into a queue structure so I didn't have to do somewhat ugly manipulations to 
discard the oldest character as I read it into a List or shift them in an array. The downside of using a queue is that
I could only access its contents by using the ToArray() method, which comes with some overhead as it needs to be
additionally copied into a string to verify if it's a valid date. I also overloaded the functionality of my custom method
so it would also return the output string for valid dates. This was just to take advantage of the fact that I'd already
converted the queue to an array and I didn't want to have to either store that array or convert the queue again.