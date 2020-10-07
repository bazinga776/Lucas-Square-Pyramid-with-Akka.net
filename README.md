# perfect-square-actor-model-f#-akka.net
Understanding parallelism through Actors in Distributed Operating Systems using F# and AKKA.NET

## Problem Definition 
An interesting problem in arithmetic with deep implications to elliptic curve
theory is the problem of finding perfect squares that are sums of consecutive
squares. 
A classic example is the Pythagorean identity:
3^2 + 4^2 = 5^2
(1): that reveals that the sum of squares of 3, 4 is itself a square. 

A more interesting example is Lucas‘ Square Pyramid:
1^2 + 2^2 + ... + 24^2 = 70^2
(2)
In both of these examples, sums of squares of consecutive integers form the square of another integer. The goal of this first project is to use F# and the actor model to build a good solution to this problem that runs well on multi-core machines.

## Project Requirements
### Input
The input provided (as command line to your program, e.g. my app) will be two numbers: N and k. The overall goal of your program is to find all k consecutive numbers starting at 1 and up to N, such that the sum of squares is itself a perfect square (square of an integer).

### Output 
On independent lines, the first number in the sequence is printed for each solution.
##### Example 1

dotnet fsi proj1.fsx 3 2
3
indicates that sequences of length 2 with start point between 1 and 3 contain
3,4 as a solution since 3^2 + 4^2 = 5^2.

##### Example 2

dotnet fsi proj1.fsx 40 24
1
indicates that sequences of length 24 with start point between 1 and 40 contain
1,2,...,24 as a solution since 1^2 + 2^2 + ... + 24^2 = 70^2

### Actor modeling 
This project exclusively utilizes the the actor facility in F# where in multiple worker actors are given a range of problems to solve and a boss that keeps track of all the problems and perform the job assignment.

## Built On
- Programming language: F# 
- Framework: AKKA.NET
- Operating System: Windows
- Programming Tool: Visual Studio Code
