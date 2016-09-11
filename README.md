# DataParsingExercise
Data parsing exercise to pull valid calendar dates from a potentially corrupted file.

The DateParser assumes the following:
1) The dates the parser is looking for match US culture norms. 
2) The input file is not so large that it can't be ingested into a string internally. 
3) The output in console is in the same format that the input is: a date of an MMDDYYYY format with a zero-based month.