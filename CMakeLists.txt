project(Shoggoth)
cmake_minimum_required(VERSION 2.6)

set(Shoggoth_CMAKE_DIR "${Shoggoth_SOURCE_DIR}/cmake" CACHE INTERNAL "")
set(CMAKE_MODULE_PATH ${CMAKE_MODULE_PATH} "${Shoggoth_CMAKE_DIR}")

include(${Shoggoth_CMAKE_DIR}/FindCSharp.cmake)
include(${Shoggoth_CMAKE_DIR}/UseCSharp.cmake)

file(MAKE_DIRECTORY ${Shoggoth_BIN_DIR}/bin)

add_custom_target(all)

subdirs(src)

#subdirs(test)

