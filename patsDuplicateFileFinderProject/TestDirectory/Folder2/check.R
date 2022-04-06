ass2data <- read.csv(file.choose(),header = TRUE)
attach(ass2data)

library(dplyr)
#remove(ass2data)
#ass2data <-ass2data %>% mutate(Gender = factor(Gender, levels = c(0, 1), labels = c("M", "F")))

#ass2data

genderGPA <- split(ass2data$GPA,ass2data$Gender)
#boxplot(genderAvs)
genderAvs <- sapply(genderGPA,mean)
genderBar <- barplot(genderAvs,xlab = "Gender",ylab = "Average GPA",main = "Average GPA by Gender",ylim =c(0,5))

text(my_bar, genderAvs , paste("n: ", ass2data$mean, sep="") ,cex=1)

genderAvs
