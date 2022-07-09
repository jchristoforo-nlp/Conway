To run my version of Conway's Game of Life

First, navigate to .... directory

Second, this console application can be run by either PowerShell or Windows cmd line.

Third, this program can run with several cmd line parameters in whichever order desired.
	Optional parameters:
		Animal draw command: draw-a "animal"
				options: owl, dog, bunny
				example: draw-a cow

		Joke command: funny

		Board refresh rate command: refresh-rate "int value"
				options: ms, s, m
				example: refresh-rate 2000ms

		Cell background color command: live-color "color"
				options: red, yellow, blue
				example: live-color "red"
				default color: black

		Cell foreground color command: dead-color "color"
				options: red, yellow, blue
				example: live-color "yellow"
				default color: white

Running example:
	Basic: dotnet conway.dll
	Full Options: dotnet conway.dll -- draw-a "bunny" funny refresh-rate 1500ms live-color blue dead-color black

