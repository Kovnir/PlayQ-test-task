# Description

[`Container`](https://github.com/Kovnir/PlayQ-test-task/blob/master/Container.cs) is a class with the looped double linked list of random bool values. You can get and change a value of the current element, move to the next element or the previous one. 

## Task

1) Create an algorithm to find the number of nodes in the list. After the search, all values should remain in the original state. You don't have to modify `Container` class or use reflection, you should find an algorithmic way to do it. Take care of performance and memory allocation. Сode should be covered with comments.
2) Create a [Unit Test](https://docs.unity3d.com/2017.4/Documentation/Manual/testing-editortestsrunner.html) for it.
3) Visualize looped list as infinite scroll view in the new [Editor Window](https://docs.unity3d.com/ScriptReference/EditorWindow.html) as shown below. `Create new` button should create new `Container` instance with random node count. The window should look the same as on gif and work correctly on any window size. You should use container instance as data provider (not create a list with same elements or something).

<img src="scroll.gif" width="400">
