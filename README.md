<a name="readme-top"></a>
<!-- PROJECT LOGO -->
<br />
<div align="center">
  <img src="https://azure.borjagq.com/resources/logo/logo@2x.png" alt="Logo" width="240">

  <br />

  <h3 align="center">Holoway: A VR Meeting Room</h3>

  <p align="center">
    Advanced Software Engineering (CS7CS3-202223), Trinity College, The University of Dublin
    <br />
    <br />
    <!--<a href="https://github.com/othneildrew/Best-README-Template">View Demo</a>-->
  </p>
  <span align="center"> 
  
  [![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)
  
  </span>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project
<!--[![Product Name Screen Shot][product-screenshot]](https://example.com)-->

The goal of this project is to create a virtual meeting application that would allow people to have remote meetings in a virtual reality environment. This application will work with a virtual reality headset and will allow the users to interact with each other and with other objects in the virtual meeting rooms. Users who don’t have a virtual reality headset will also be able to join the conference rooms via a web application and see the virtual view in their web browser (and possibly interact with it). Users will also be able to share content with each other like sharing their desktop screen, sharing their webcam or sharing some files with each other. It will be possible to have multiple meetings taking place at the same time in different meeting rooms.

In the application, each user will be able to create or join a meeting room (via the web app or the headset app), see and hear their colleagues and interact with them in real-time, share some content via their web application (from their desktop), interact with other objects (some interaction functionalities like drawing in VR, playing some games and manipulating objects) and move inside the meeting room, use a Google Drive integration to share and save files, and leave the meeting room. The users would also be able to join the meeting with both apps at the same time (to be able to use both apps functionalities)

Some of the major challenges of this project include the networking part where we allow multiple people to join the virtual meeting rooms, the screen streaming and webcam streaming, sharing the gesture tracking and head movement tracking of the people in the meeting, allowing users to draw in real-time and to make the application cross-platform between the web browser and VR. And If time allows it, we would also like to work on rendering real-time avatars and lip-synching with ambient audio (audio that takes into account users positions) and potentially integrate a web browser inside the application.

The meeting room should:
 
* Support interaction with the environment through a headset: For example, the user should be able to write on a blackboard existing in the VR scene;
* From a web application, enable a user to look around the VR scene, and have interactions similar to the headset application;
* Support the hharing documents (Web and VR). Both applications should be able to share the documents with the other users in the room;
* Support sharing webcam, screen, and voice (WebApp);
* Support a connection between the WebApp and VR app;
* Support the user to see what their teammates are doing in real time (their exact motion in real-time);
* Support multiple users joining the meeting rooms, with voice transmission.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

</br>

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)

This project has been developed using Unity version **2021.3.17f1**.



<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

1. Install Unity editor version **2021.3.17f1** using [Unity Hub](https://docs.unity3d.com/2018.2/Documentation/Manual/GettingStartedInstallingHub.html). 


### Installation

_Below is an example of how you can instruct your audience on installing and setting up your app. This template doesn't rely on any external dependencies or services._

1. Clone the repo
   ```sh
   git remote add origin https://gitlab.scss.tcd.ie/boulahcn/ase-team3-meeting_room.git
   ```
2. Open the project in Unity.
3. Build and run the project from Unity editor.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ROADMAP -->
## Current sprint

### Pairs

* Pair 1: Siddharth Shenoy, Hamza Gabajiwala
* Pair 2: Nour Boulahcen, Borja García Quiroga
* Pair 3: Shuo Jia, Jiyuan Liu, Stefan-Catalin Iscru-Togan

### Roadmap

- [x] General refactoring (ALL)
- [x] Review tests (ALL)
- [x] Login linked with menu (NB+BGQ)
- [x] Reset cookies when logout 
- [ ] Whiteboard complex algorithm (NB+BGQ)
- [ ] Voice implementation (SJ+JL)
- [ ] Multiplayer in VR (HG+SS)
- [ ] All functionalities in VR (SCIT)
- [ ] Report writing (ALL)
- [ ] Raytracing (NB+BGQ)
- [ ] Shared documents (using GDocs or whatever)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## Acknowledgement

This project has been developed in partial fulfillment of the requirements for the MSc in Computer Science, Augmented and Virtual Reality at Trinity College, The University of Dublin, 2022-2023.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Authors

* Nour Boulahcen (boulahcn@tcd.ie)
* Hamza Gabajiwala (gabajiwh@tcd.ie)
* Borja García Quiroga (garcaqub@tcd.ie)
* Stefan-Catalin Iscru-Togan (iscrutos@tcd.ie)
* Shuo Jia (jiash@tcd.ie)
* Jiyuan Liu (jliu7@tcd.ie)
* Siddharth Shenoy (shenoys@tcd.ie)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
