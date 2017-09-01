# RenameHistogramDiffs #
The idea is to programmatically sort images based on their histogram.  
In this program it is done by resizing the image to a 16x16 bitmap, take the histogram of said bitmap, then finding the difference between the values in the histogram and rename the input file
with the difference.  
The current filename length is 100 characters, chosen to comply with the ~250 character limit windows imposes on directory+filename length. 
I have not done any testing to see how many characters is required. 



## Usecase ##
Currently (sept 2017), the program requires a bat file to be run in order to rename files in a directory.  
Curent command: RenameHistogramdiffs input.jpg  
I am a big believer of the shotgun approach, meaning that in the future I would like to just run RenameHistogramdiffs and it would automatically rename all the files in the current directory.  


## TODO ##
Rename all files in the current directory  
If file already exists, rename to output and a increasing integer.
