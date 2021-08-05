# High Contrast Mode Unity package

This High Contrast Mode Unity package was created as a Medialogy bachelour project. 
The goal of this project was to simplify the process developers have to go through to implement for accessibility.
We propose a Unity package meant to ease the implementation of a high-contrast accessibility feature for digital games, which is mostly benefitial for people with low vision.
It is an open-source project, so everybody is welcome to contribute and expand upon accessibility features included. 

## About High Contrast Mode

The High Contrast Mode package can be helpful for making your project accessible to people with low-vision. 
High Contrast Mode is easy to implement and saves developers time when designing for accessibility.

High Contrast Mode expands the renderer to enhance certain objects in the scene (e.g. enemies, allies, collectables). 
The package uses a custom render feature and shader graph, which blend rendered objects with HighContrastShader.shadergraph with adjustable properties. 
The custom render feature uses layers to identify objects: all objects assigned to the same layer will have the same color.

*Examples:*

![Image][1]

![Image][2]

## Requirements

This High Contrast Mode version 1.0.0 is compatible with the following versions of the Unity Editor:

- 2020.3 and later (recommended)

To use this package, you must meet the following requirements:

- Have project with Universal Render Pipeline (URP)
- Shader graph package (part of URP)

## Known Limitations

The High Contrast Mode version 1.0.0 includes the following know limitations:

- Not optimal for release due to high performance costs
- It only supports opaque objects (no support for i.e. grass, transparency and skybox)

## Installing High Contrast Mode

It is recommended to install the High Contrast Mode package from disk. 
On how to install packages, please refer to the official Unity Manual
(https://docs.unity3d.com/Manual/upm-ui-local.html)

## Using High Contrast Mode

The High Contrast Mode is applied as a renderer feature in *Forward Renderer Data* which is part of *Pipeline Asset*.
If the project does not have any *Pipeline Asset* you can create a new one by going to *Assets -> Create -> Rendering -> Universal Render Pipeline -> Pipeline Asset (Forward Renderer).* 
This will automatically create *Forward Renderer Data.*
For more detailed information on custom render pipeline refer to official Unity Manual (https://docs.unity3d.com/Manual/srp-custom.html)
In the generated *Forward Renderer Data* press the button “Add Renderer Feature” and choose **High Contrast Mode** from the list of dropdown menu items.

The number of enhanced groups of objects can be specified in the **Overrides** value. 
Each of these groups are represented as array list *Elements*. 
Every *Element* contains *Layer Mask, Color, Outline Thickness, Outline Brightness* and *Shader Transparency.*

![Image][3]

*Layer mask* - specify the layer of the assigned object which should be changed.

*Color* - specify the color of assigned objects.

*Outline thickness* - changes the outline thickness of the selected objects.

*Outline brightness* - changes the outline brightness of the selected objects.

*Shader transparency* - specify the amount of blending between the rendered objects and color layer.

On more detailed information about how to work with layers in Unity please refer to the official Unity manual 
(https://docs.unity3d.com/Manual/Layers.html)



[1]:https://github.com/kgardarsson/high-contrast-mode/blob/main/images/image3.png
[2]:https://github.com/kgardarsson/high-contrast-mode/blob/main/images/image2.png
[3]:https://github.com/kgardarsson/high-contrast-mode/blob/main/images/image1.png
