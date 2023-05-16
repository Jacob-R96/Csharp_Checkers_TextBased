# personal1

Within this file I used a text based method to give the conecept of checkers. Its divided into three classes One being the main form that uphold majority of the functionality of the checker pieces movements and execution.
Second being the Board class that helps the Cell class give a X and a Y on the 8x8 checker grid. 

For my methods KingMovement, RedMovement, and BlackMovement

  The method takes several parameters passed by reference (ref). 
                  These parameters include currentcell of type Cell, Switch of type bool, x and y of type int, and RedLeft, RedRight, BlackLeft, and BlackRight of type bool.

  The method calls a method called CheckerNextLegalMove on an object named myBoard. It passes the currentcell parameter, along with other boolean parameters (RedLeft, RedRight, BlackLeft, and BlackRight). The purpose of this method is to determine the legal moves for a checker piece and update some internal data structures.

  The code initializes a variable Colorr of type Color that iniaties the radio switches back and forth between the black and red colors.
               If radioButton1 is checked, Colorr is set to Color.Black.
               If radioButton2 is checked, Colorr is set to Color.Red.

  The code checks the state of a checkbox (booldoublejump) and enters a conditional block if it is not checked (false).
          Inside the conditional block, the code tries to update the text and color of certain buttons (btnGrid) based on the values of RedLeft, RedRight, BlackLeft, and BlackRight. 
                  If the conditions are met, the respective buttons are assigned  with the specific texts and proper colors.

  The code then enters a nested loop that iterates over the 2D btnGrid array.
                Within the nested loop, the code performs various operations on the buttons based on their current texts and colors. It updates the texts and colors of the buttons based on certain conditions, and wipes the board of any illegal pieces, or movements on the board. 

  For KINGS to move 
      If a button has the text "Legal", the variable Switch is set to true, and the button's text is changed to a "(K)" and its color is set to its proper color.

  Similar logic is applied for buttons with the texts "1", "2", "3", and "4". (These number are only for attack) 
      Which represents movment top left, top right, bottom right, bottom left.

  The corresponding buttons are cleared, and if any of the flags (RedLeft, RedRight, BlackLeft, or BlackRight) are true and Switch is true, it will see if the doublejumpswitch flag is set to true.
      If the all correspondance comes out to be true then the doublejump method will take affect

  If a button has the text "()", it is changed to "(K)".

  If doublejumpswitch is true, the method Clearlegals is called, and the buttons are updated with texts and colors based on the values of RedRight, RedLeft, BlackLeft, and BlackRight. The current button is set to "Clicked", and booldoublejump.Checked is set to true.

  If Switch is true and doublejumpswitch is false, the Clearlegals method is called, and the radio button and boolKingCheck are updated based on the Colorr value.
 
 Clear Legal method reset the board of any illegal objects, and movements. Right before giving the next player turn. 

