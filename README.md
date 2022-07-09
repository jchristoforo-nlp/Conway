# Conway
Quarterly Goal

To run my version of Conway's Game of Life

First, create directory on local computer

Second, run GitBash "git clone https://github.com/jchristoforo-nlp/Conway.git" creted in First step.

Third, navigate to net6.0 direcory inside directory created in Second step.

Fourth, open Cmd window. Easiest is to click in Windows Explorer navigational bar once in net6.0 directory and type cmd in navigation and hit enter.
 

Help section
------------
This console application can be run by either PowerShell or Windows cmd line.

This program can run with several optional cmd line parameters in whichever order desired.
	Optional commands and parameters:
		Animal draw command: draw-a "animal"
				needs parameter : yes. default if not provided or animal not recognized is no animal drawing
				options: owl, dog, bunny
				example: draw-a cow

		Joke command: funny
				needs parameter: no.

		Board refresh rate command: refresh-rate "int value"
				needs parameter: yes. default is no delay
				options: ms, s, m
				example: refresh-rate 2000ms

		Cell live color command: live-color "color"
				needs parameter: yes. default color: white
				options: red, yellow, blue
				example: live-color "red"
				
		Cell dead color command: dead-color "color"
				needs parameter: yes. default color: black
				options: red, yellow, blue
				example: dead-color "yellow"
				
Running example:
	Basic: dotnet conway.dll
	Full Options: dotnet conway.dll -- draw-a "bunny" funny refresh-rate 1500ms live-color blue dead-color yellow
