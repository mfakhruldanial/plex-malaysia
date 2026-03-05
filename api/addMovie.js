export default async function handler(req,res){

if(req.method !== "POST") return res.status(405).end()

const movie = req.body

// Normally you'd save to DB here
// For demo we return success

res.status(200).json({success:true})

}