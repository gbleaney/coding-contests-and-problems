# Minglish lesson
# ===============
#
# Welcome to the lab, minion. Henceforth you shall do the bidding of Professor Boolean. Some say he's mad, trying to
# develop a zombie serum and all... but we think he's brilliant!
#
# First things first - Minions don't speak English, we speak Minglish. Use the Minglish dictionary to learn! The first
# thing you'll learn is how to use the dictionary.
#
# Open the dictionary. Read the page numbers, figure out which pages come before others. You recognize the same letters
# used in English, but the order of letters is completely different in Minglish than English (a < b < c < ...).
#
# Given a sorted list of dictionary words (you know they are sorted because you can read the page numbers), can you find
# the alphabetical order of the Minglish alphabet? For example, if the words were ["z", "yx", "yz"] the alphabetical
# order would be "xzy," which means x < z < y. The first two words tell you that z < y, and the last two words tell you
# that x < z.
#
# Write a function answer(words) which, given a list of words sorted alphabetically in the Minglish alphabet, outputs a
# string that contains each letter present in the list of words exactly once; the order of the letters in the output
# must follow the order of letters in the Minglish alphabet.
#
# The list will contain at least 1 and no more than 50 words, and each word will consist of at least 1 and no more than
# 50 lowercase letters [a-z]. It is guaranteed that a total ordering can be developed from the input provided (i.e.
# given any two distinct letters, you can tell which is greater), and so the answer will exist and be unique.
#
# Languages
# =========
#
# To provide a Python solution, edit solution.py
# To provide a Java solution, edit solution.java
#
# Test cases
# ==========
#
# Inputs:
#     (string list) words = ["y", "z", "xy"]
# Output:
#     (string) "yzx"
#
# Inputs:
#     (string list) words = ["ba", "ab", "cb"]
# Output:
#     (string) "bac"

lesser_letters = {}

def answer(words):
    extract_orderings([word for word in words if word])

    reverse_ordering = ""
    next_letter = ""
    while len(lesser_letters):
        next_letter = find_remove_least(next_letter)
        reverse_ordering += next_letter

    return reverse_ordering[::-1]


def find_remove_least(least):
    result = None
    for letter, lessers in lesser_letters.items():
        lesser_letters[letter].discard(least)
        if len(lesser_letters[letter]) == 0:
            del lesser_letters[letter]
            assert not result
            result = letter

    return result

def extract_orderings(words):
    prev_letters = words[0][0]

    if words[0][0] not in lesser_letters:
        lesser_letters[words[0][0]] = set()

    next_words = []

    for word in words:
        if prev_letters.endswith(word[0]):
            if len(word) > 1:
                next_words.append(word[1:])

        else:
            if len(next_words) > 1:
                extract_orderings(next_words)

            if len(word) > 1:
                next_words = [word[1:]]
            else:
                next_words = []

            current_letter = word[0]

            if current_letter not in lesser_letters:
                lesser_letters[current_letter] = set()

            for letter in prev_letters:
                lesser_letters[letter].add(current_letter)

            prev_letters += current_letter


    if len(next_words) > 1:
         extract_orderings(next_words)



assert answer(["a", "b", "ba", "ba"]) == "ab"
assert answer(["aa", "ab", "ab", "ab"]) == "ab"
assert answer(["ab", "ab", "ab", "ab", "b"]) == "ab"
assert answer([u"a"]) == "a"
assert answer([u"aa", u"abb", u"abc"]) == "abc"
assert answer(["a", "aab", "aac", "abc"]) == "abc"
assert answer(["bab", "bad", "baa", "ab", "cb"]) == "bdac"
assert answer(["y", "z", "xy"]) == "yzx"
assert answer(["ba", "ab", "cb"]) == "bac"
assert answer([u"aaaaa", u"aaaaaaa", u"a"]) == "a"
assert answer(["aaaaa", "aaabaaa", "a"]) == "ab"
assert answer([u"aaaaa"]) == "a"
assert answer([u'bab', u'bad', u'baa', u'ab', u'cb']) == "bdac"
assert answer(["bab" ,"bab", "bad", "bada", "baa", "ab", "cb"]) == "bdac"
assert answer(["ac", "ad", "bb", "bc"]) == "abcd"
assert answer(["d", "dc", "db", "cb", "ca"]) == "dcba"
assert answer(["a", "s", "d", "f", "g"]) == "asdfg"

print("Done!")