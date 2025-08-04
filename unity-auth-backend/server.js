const express = require("express")
const mongoose = require("mongoose")
const router = require("./routes/user")
const errorHandler = require("./middlewares/errorHandle")



const app = express()
const port = 8000


// connected with mongodb
try {
mongoose.connect("mongodb+srv://drawing192:Randyort2002@cluster1.nkiqwk5.mongodb.net/unityLogin?retryWrites=true&w=majority&appName=Cluster1")
console.log("DB connected with server well");
} catch (error) {
    console.log(error);
}



app.use(express.json())

// routes

app.use("/", router)

// error handling
app.use(errorHandler)

app.listen(port, () => {
    console.log("server is running on", port);
})
