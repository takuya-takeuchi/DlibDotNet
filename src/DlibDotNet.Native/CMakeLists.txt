cmake_minimum_required(VERSION 3.0.0)
set(PROJ_NAME DlibDotNet.Native)

project(${PROJ_NAME} VERSION 0.0.0)

if(NOT CMAKE_GENERATOR MATCHES "Visual Studio")
    set(CMAKE_CXX_STANDARD 11)  # for c++11
    find_package(X11 REQUIRED)
    link_libraries(${X11_LIBRARIES})
    include_directories(${X11_INCLUDE_DIR})
endif()

# Select the release build type by default
if (NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE "Release")
endif()

# Support to load image
# NOTE: In other words, dlib can NOT load image if these options are not enabled.
add_definitions(-DDLIB_JPEG_SUPPORT)
add_definitions(-DDLIB_PNG_SUPPORT)
add_definitions(-DDLIB_GIF_SUPPORT)

# Ignore GUI Support
if (NOT WIN32)
    add_definitions(-DDLIB_NO_GUI_SUPPORT)
endif()

# include
# NOTE: You should *NOT* add the dlib folder itself to your compiler's include path.
set(INCLUDE_DIR
    ${DLIB_PATH}
    ${DLIB_PATH}/dlib/external/libjpeg
    ${DLIB_PATH}/dlib/external/libpng
    ${DLIB_PATH}/dlib/external/zlib
    ${GIFLIB_PATH}/lib
)
include_directories("${INCLUDE_DIR}")

include(CTest)
enable_testing()

# target source and header files
FILE(GLOB_RECURSE HEADERS ${PROJECT_SOURCE_DIR}/*.h)
FILE(GLOB_RECURSE SOURCES ${PROJECT_SOURCE_DIR}/*.cpp)
FILE(GLOB_RECURSE DLIB_SOURCES ${DLIB_PATH}/dlib/all/*.cpp)
FILE(GLOB_RECURSE JPEG_SOURCES ${DLIB_PATH}/dlib/external/libjpeg/*.cpp)
FILE(GLOB_RECURSE PNG_SOURCES ${DLIB_PATH}/dlib/external/libpng/*.c)
FILE(GLOB_RECURSE ZLIB_SOURCES ${DLIB_PATH}/dlib/external/zlib/*.c)
FILE(GLOB_RECURSE GIFLIB_SOURCES ${GIFLIB_PATH}/lib/*.c)
add_library(${PROJ_NAME} SHARED ${HEADERS} ${SOURCES} ${DLIB_SOURCES} ${JPEG_SOURCES} ${PNG_SOURCES} ${ZLIB_SOURCES} ${GIFLIB_SOURCES})

# lib
# NOTE: These statement should be at after add_library
# NOTE: You must add preprocessor DLIB_JPEG_SUPPORT and DLIB_PNG_SUPPORT to DlibDotNet.Native.vcxproj if you wanna load png and jpeg
if(CMAKE_GENERATOR MATCHES "Visual Studio")
  set(CMAKE_CXX_FLAGS "${CMAKE_C_FLAGS} /bigobj")
  set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /EHsc")
  set(STATIC_LIBRARIES
      ${DLIB_LIBPATH}/dlib.lib
  )
  target_link_libraries(${PROJ_NAME} ${STATIC_LIBRARIES})
else()
  set(STATIC_LIBRARIES
      ${DLIB_LIBPATH}/libdlib.a
  )
  target_link_libraries(${PROJ_NAME} ${STATIC_LIBRARIES})
endif()

# Enforce /MD option
set(CompilerFlags
    CMAKE_CXX_FLAGS
    CMAKE_CXX_FLAGS_DEBUG
    CMAKE_CXX_FLAGS_RELEASE
    CMAKE_C_FLAGS
    CMAKE_C_FLAGS_DEBUG
    CMAKE_C_FLAGS_RELEASE
)
foreach(CompilerFlag ${CompilerFlags})
string(REPLACE "/MDd" "/MD" ${CompilerFlag} "${${CompilerFlag}}")
endforeach()

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
