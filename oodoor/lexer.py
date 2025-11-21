import sys
import os
import string

class lexer:

    def __init__(self, file) -> None:
        self.file = file

    # define the x and y position within a file
    def get_postition(self) -> tuple[int, int]:
        return 1, 2

    def tokenize(self, *pos) -> str: 
        return ""

