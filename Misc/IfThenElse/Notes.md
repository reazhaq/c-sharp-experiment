flavors of if-else pattern

* too many if-else branches - example: `TooManyIfStatements.cs`
* multiple if branches with enum - example: `CleanCodeUsingEnumeration.cs`
* lots of nested conditions - if this and that but not the other - can I use truth table here? example: `DecisionTable.cs`
  - pattern look more like `approved` or `not` based on lots and lots of boolean variable states. Best to start with a decision table, like a truth table

  | New Customer | Has Coupon | Existing Customer | Elite Customer | Over $200 | Over $500 | Free Shipping |
  | --- | --- | --- | --- | --- | --- | --- |
  | no | no | no | no | no | no | no |
  | yes | no | no | no | no | no | no |
  | yes | yes | no | no | no | no | yes |
  | - | yes | yes | no | no | no | yes |
  | - | no | yes | no | yes | no | no |
  | - | no | yes | no | no | yes | yes |
  | - | - | - | yes | no | no | yes |

this sort of all true/false can be coded in to a boolean array, where index of the array is number constructed from 1s and 0s (for yes and no) and get their decimal value. For example, first row, all 0s, gives you 0 index. Next row (yes, no, no, no, no, no => 100000) becomes 32, lookup index 32. 3rd row (yes, yes, no, no, no, no => 110000) becomes 48, lookup index 48. Concept is simple, but implementation looks not much friendly - combining all those to a binary number to find the index part is kind of nasty. Lookup is O(1) and memory usage is constant (though lots of extra)

Similar pattern but little more complex, not all of them are yes-no, but some have 3 kinds of values (like destination below). This can be converted into a decision table, just have to use a terciary number system - that's not very user friendly.
  - imagine making a decision about travel expense - either approval or not; or looking up expense account number

| group travel | travel type | travel reason | destination | expense account|
| --- | --- | --- | --- | --- |
| yes | student | field trip | in-state | 0001 |
| yes | student | field trip | out-of-state | 0002 |
| yes | student | field trip | international | 0003 |
| yes | teacher | seminar | in-state | 0003 |
| yes | teacher | personal | international | 0004 |

We can make decision table like above using switch statements

* visitor pattern - printing document using correct printer - more of single responsibility - example: `LetExpertDoTheWork.cs`
* state machine - for certain - certain condition object moves from one state to another. state machines are everywhere - like ATM machine, elevator, climbing stairs...
  - like and dislike count
    - start state
    - user clicks `like`
      - if in start-state, increase the like count, move to like-state
      - if in like-state, decrease the like count, move to start-state
      - if in dislike-state, decrease the like count, increase dislike count, move to dislike-state
    - user clicks `dislike` button
      - if in start-state, increase the dislike count, move to dislike-state
      - if in like-state, decrease the like count, increase dislike count, move to dislike-state
      - if in dislike-state, decrease dislike count, move to start-state
    - like and dislike count goes down only if count is positive
* chain of processing - one finishes and passes on to another. examples like order taken, goes to payment, goes to inventory, goes to shipping, etc. these are best cases for a true distributed (msg base) system for scalability
