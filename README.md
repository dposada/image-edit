Image Edit
==========
Image Edit is a very simple Windows Forms app written in C# that allows you to bulk-crop images in a directory. In particular, you specify the following:

- The root directory in which the source images live
- The x-coordinate of the point to start cropping
- The y-coordinate of the point to start cropping
- The width to crop
- The height to crop
- The wildcards to use for including files (i.e. *.jpg)

Note that (0,0) is in the top left and positive y-values go down rather than up.

The main use case for using this program is when you have many images that all need the same section cropped out (for example, patient ultrasound images where you want to remove the top bar containing identifying information).
