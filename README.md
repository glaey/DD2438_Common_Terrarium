# DD2438_Common_Terrarium
 Common Terrarium for the DD2438 course 2020
 
 Updates:
 
 CreatureAI is now logic empty, it works as virtual and is supposed to be inherited by all your AnimalAI classes.
 SpecieName and specieId parameters have been added to tell different species.
 updateStats virtual method has been added.
 
 Creature has a new parameter Generation.
 
 GameManager initialize specieID when instantiating the creatures and call updateStats() on every species each update.
 
 AsexualMutantReproduction is a new implementation of IReproduction. It gives random changes to creatures parameters.
 
 CostFunctionV2 is a new implementation of ICostFunction with different wheigts (still not perfectly tuned).
 
 ISensorV2 is a new interface that extend ISensor with usefull methods as SensePredators.
 
 CircularSensorV2 is an implementation of ISensorV2.
