# UIRenderExample
This is a small C# project demonstrating how one would parse and organize a GUI from an XML file. It is meant to be used together with the corresponding Gamasutra article (soon).

# What it does

 - Parse an XML file
 - Create all UI Elements according to their definition
 - Organize all UI Elements in a visual tree
 - Display the UI

The following UI Elements are included:

 - *Div* / *Stackpanel* - for organizing your UI Elements horizontally or vertically
 - *Button* - with click events and effects
 - *Label* - for displaying text
 - *Image* - for displaying Images

Each Element is able to be styled to some basic extent (background color, border color, margins, alignments). 

# What it doesn't

Rendering is done via C# WPF... Cheating so to speak. But the rendering aspect is not the focus here and we only require basic capabilities like drawing boxes and text.

# Example XML
The following example XML is included in the project and should give a rough overview of what is possible with this small project

```xml
<?xml version="1.0" encoding="UTF-8"?>
<body margin="20">
	<div style="orientation: horizontal;">
		<div style="orientation: vertical; background: #AA8888;">
			<label name="lbl_title" style="background: #FFFFFF; align: center;" margin="10">Test</label>
			<image margin="10">kermit.png</image>
		</div>
		<div style="orientation: vertical; border: #000000" margin="5">
			<button click="btn1_click">Test 1</button>
			<button click="btn2_click">Test 2</button>
		</div>
	</div>
</body>
```
