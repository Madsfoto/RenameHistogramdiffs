# RenameHistogramDiffs #
The idea is to programmatically sort images based on their histogram.  
In this program it is done by resizing the image to a 16x16 bitmap, take the histogram of said bitmap, then finding the difference between the values in the histogram and rename the input file
with the difference.  
The current filename length is 100 characters, chosen to comply with the ~250 character limit windows imposes on directory+filename length. 
I have not done any testing to see how many characters is required. 



## Usecase ##
RenameHistogramdiffs 
I am a big believer of the shotgun approach, meaning that it automatically renames all the files in the current directory.  


## TODO ##

If file already exists, rename to output and a increasing integer.
